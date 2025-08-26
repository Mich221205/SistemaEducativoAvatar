using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OfertaController : ControllerBase
{
    private readonly DBContext _db;
    public OfertaController(DBContext db) => _db = db;

    public class OfertaItemDto
    {
        public int IdGrupo { get; set; }
        public string CodigoMateria { get; set; } = "";
        public string NombreMateria { get; set; } = "";
        public string Requisito { get; set; } = "Ninguno";
        public string GrupoNumero { get; set; } = "";
        public string Horario { get; set; } = "";
    }

    [HttpGet("carrera/{idCarrera:int}/periodo/actual")]
    public async Task<ActionResult<IEnumerable<OfertaItemDto>>> GetPorCarreraPeriodoActual(int idCarrera)
    {
        try
        {
            // Período actual
            var periodo = await _db.Periodo_Lectivo
                .OrderByDescending(p => p.anio)
                .ThenByDescending(p => p.cuatrimestre)
                .FirstOrDefaultAsync();
            if (periodo == null) return Ok(new List<OfertaItemDto>());

            // ----- REQUISITOS -----
            var reqDict = await
            (
                from r in _db.Requisitos.AsNoTracking()
                join m in _db.Materias.AsNoTracking() on r.IdRequisito equals m.IdMateria
                group m.Nombre by r.IdMateria into g
                select new
                {
                    IdMateria = g.Key,
                    Lista = g.Where(n => !string.IsNullOrWhiteSpace(n))
                             .Distinct()
                             .OrderBy(n => n)
                             .ToList()
                }
            ).ToDictionaryAsync(x => x.IdMateria, x => x.Lista);

            // ----- CORREQUISITOS -----
            var coDict = await
            (
                from c in _db.Correquisitos.AsNoTracking()
                join m in _db.Materias.AsNoTracking() on c.IdCorrequisito equals m.IdMateria
                group m.Nombre by c.IdMateria into g
                select new
                {
                    IdMateria = g.Key,
                    Lista = g.Where(n => !string.IsNullOrWhiteSpace(n))
                             .Distinct()
                             .OrderBy(n => n)
                             .ToList()
                }
            ).ToDictionaryAsync(x => x.IdMateria, x => x.Lista);

            string BuildReqText(int idMateria)
            {
                reqDict.TryGetValue(idMateria, out var reqs);
                coDict.TryGetValue(idMateria, out var cores);
                reqs ??= new List<string>();
                cores ??= new List<string>();

                var parts = new List<string>();
                if (reqs.Count > 0) parts.Add("Req: " + string.Join(", ", reqs));
                if (cores.Count > 0) parts.Add("Co-req: " + string.Join(", ", cores));

                return parts.Count == 0 ? "Ninguno" : string.Join(" | ", parts);
            }

            // ----- HORARIOS: Diccionario IdGrupo -> "Lunes 08:00 – 10:00 / Jueves 10:00 – 12:00" -----
            // Si HoraInicio/HoraFin fueran TimeSpan? (nullable), usa HasValue como en el bloque comentado.
            var horariosDict = await
            (
                from h in _db.Horarios.AsNoTracking()
                group h by h.IdGrupo into g
                select new
                {
                    IdGrupo = g.Key,
                    Texto = string.Join(" / ",
                        g.OrderBy(x => x.DiaSemana)
                         .ThenBy(x => x.HoraInicio)
                         .Select(x => $"{(x.DiaSemana ?? "").Trim()} {x.HoraInicio:hh\\:mm} – {x.HoraFin:hh\\:mm}"))
                }
            ).ToDictionaryAsync(x => x.IdGrupo, x => x.Texto);

            // Si son TimeSpan? (nullable), usa esto en vez del bloque anterior:
            /*
            var horariosDict = await
            (
                from h in _db.Horarios.AsNoTracking()
                group h by h.IdGrupo into g
                select new
                {
                    IdGrupo = g.Key,
                    Texto = string.Join(" / ",
                        g.OrderBy(x => x.DiaSemana)
                         .ThenBy(x => x.HoraInicio)
                         .Select(x =>
                         {
                             var ini = x.HoraInicio.HasValue ? x.HoraInicio.Value.ToString(@"hh\:mm") : "";
                             var fin = x.HoraFin.HasValue   ? x.HoraFin.Value.ToString(@"hh\:mm")   : "";
                             return $"{(x.DiaSemana ?? "").Trim()} {ini} – {fin}";
                         }))
                }
            ).ToDictionaryAsync(x => x.IdGrupo, x => x.Texto);
            */

            // ----- Oferta base: grupos + materias (sin depender del join de horarios) -----
            var baseRows = await
            (
                from g in _db.Grupos
                join m in _db.Materias on g.IdMateria equals m.IdMateria
                join pe in _db.Plan_Estudio on m.IdPlan equals pe.id_plan
                where pe.id_carrera == idCarrera
                select new { g.IdGrupo, g.GrupoNumero, m.IdMateria, m.Codigo, m.Nombre }
            )
            .AsNoTracking()
            .ToListAsync();

            var list = baseRows
                .GroupBy(x => new { x.IdGrupo, x.IdMateria, x.Codigo, x.Nombre, x.GrupoNumero })
                .Select(grp =>
                {
                    var tieneHorario = horariosDict.TryGetValue(grp.Key.IdGrupo, out var horaTxt);
                    return new OfertaItemDto
                    {
                        IdGrupo = grp.Key.IdGrupo,
                        CodigoMateria = grp.Key.Codigo,
                        NombreMateria = grp.Key.Nombre,
                        GrupoNumero = grp.Key.GrupoNumero ?? "",
                        Horario = tieneHorario ? horaTxt : "",        // o "Sin horario"
                        Requisito = BuildReqText(grp.Key.IdMateria)
                    };
                })
                .OrderBy(o => o.CodigoMateria)
                .ToList();

            return Ok(list);
        }
        catch (Exception ex)
        {
            return Problem($"Error al generar la oferta: {ex.Message}");
        }
    }

}
