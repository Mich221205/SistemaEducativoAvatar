using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;

[ApiController]
[Route("api/[controller]")]
public class NotasController : ControllerBase
{
    private readonly DBContext _db;
    public NotasController(DBContext db) => _db = db;

    public class NotaFullVM
    {
        public int IdEstudiante { get; set; }
        public string Estudiante { get; set; } = "";
        public int IdMatricula { get; set; }
        public string Materia { get; set; } = "";
        public string Grupo { get; set; } = "";
        public decimal Nota { get; set; }
        public string Condicion { get; set; } = "";
    }
    public class NotaFilaVM
    {
        public int IdDetalle { get; set; }     // Detalle_Matricula (PK)
        public string Carne { get; set; } = ""; // si lo tienes en Estudiante
        public string Nombre { get; set; } = ""; // Usuario.Nombre (+ Apellidos si aplica)
        public string Materia { get; set; } = "";
        public string Grupo { get; set; } = "";
        public decimal? Nota { get; set; }     // puede venir null si no registrada
    }

    public class NotaEditVM
    {
        public int IdDetalle { get; set; }
        public decimal? Nota { get; set; }     // 0–100 (o null si quieres limpiar)
    }

    // GET api/Notas/todas?soloActual=true
    [HttpGet("todas")]
    public async Task<IActionResult> GetTodas()
    {
        var query =
            from d in _db.Detalle_Matriculas.AsNoTracking()
            join g in _db.Grupos.AsNoTracking() on d.IdGrupo equals g.IdGrupo
            join mat in _db.Materias.AsNoTracking() on g.IdMateria equals mat.IdMateria
            join m in _db.Matriculas.AsNoTracking() on d.IdMatricula equals m.id_matricula
            join e in _db.Estudiantes.AsNoTracking() on m.id_estudiante equals e.IdEstudiante
            join u in _db.Usuarios.AsNoTracking() on e.IdUsuario equals u.IdUsuario
            select new NotaFullVM
            {
                IdEstudiante = e.IdEstudiante,
                Estudiante = (u.nombre),
                IdMatricula = d.IdMatricula,
                Materia = mat.Nombre,
                Grupo = g.GrupoNumero,   // si tu entidad tiene otro nombre, cámbialo aquí
                Nota = d.Nota,
                Condicion = d.Condicion
            };

        var listado = await query.ToListAsync();
        return Ok(listado);
    }
    [HttpGet("debug-resumen")]
    public async Task<IActionResult> DebugResumen()
    {
        var totalDetalles = await _db.Detalle_Matriculas.CountAsync();
        var totalGrupos = await _db.Grupos.CountAsync();
        var totalMaterias = await _db.Materias.CountAsync();
        var totalMatriculas = await _db.Matriculas.CountAsync();
        var totalEstudiantes = await _db.Estudiantes.CountAsync();
        var totalUsuarios = await _db.Usuarios.CountAsync();

        // ¿Cuántos detalles tienen grupo existente?
        var detallesConGrupo = await (
            from d in _db.Detalle_Matriculas
            join g in _db.Grupos on d.IdGrupo equals g.IdGrupo
            select d.IdDetalle
        ).CountAsync();

        // ¿Cuántos detalles llegan hasta Materia?
        var detallesHastaMateria = await (
            from d in _db.Detalle_Matriculas
            join g in _db.Grupos on d.IdGrupo equals g.IdGrupo
            join m in _db.Materias on g.IdMateria equals m.IdMateria
            select d.IdDetalle
        ).CountAsync();

        // ¿Cuántos detalles llegan hasta Estudiante?
        var detallesHastaEstudiante = await (
            from d in _db.Detalle_Matriculas
            join mat in _db.Matriculas on d.IdMatricula equals mat.id_matricula
            join e in _db.Estudiantes on mat.id_estudiante equals e.IdEstudiante
            select d.IdDetalle
        ).CountAsync();

        // ¿Cuántos llegan a Usuario?
        var detallesHastaUsuario = await (
            from d in _db.Detalle_Matriculas
            join mat in _db.Matriculas on d.IdMatricula equals mat.id_matricula
            join e in _db.Estudiantes on mat.id_estudiante equals e.IdEstudiante
            join u in _db.Usuarios on e.IdUsuario equals u.IdUsuario
            select d.IdDetalle
        ).CountAsync();

        return Ok(new
        {
            tablas = new
            {
                totalDetalles,
                totalGrupos,
                totalMaterias,
                totalMatriculas,
                totalEstudiantes,
                totalUsuarios
            },
            enlaces = new
            {
                detallesConGrupo,
                detallesHastaMateria,
                detallesHastaEstudiante,
                detallesHastaUsuario
            }
        });
    }
    // POST api/Notas/guardar-por-grupo/123
    [HttpPost("guardar-por-grupo/{idGrupo:int}")]
    public async Task<IActionResult> GuardarPorGrupo(int idGrupo, [FromBody] List<NotaEditVM> cambios)
    {
        if (cambios == null || cambios.Count == 0) return BadRequest("No hay cambios.");

        // valida rango 0–100 (opcional)
        foreach (var c in cambios)
        {
            if (c.Nota is < 0 or > 100) return BadRequest($"Nota inválida para IdDetalle {c.IdDetalle}.");
        }

        // trae los detalles del grupo y aplica cambios
        var ids = cambios.Select(c => c.IdDetalle).ToHashSet();
        var detalles = await _db.Detalle_Matriculas
            .Where(d => d.IdGrupo == idGrupo && ids.Contains(d.IdDetalle))
            .ToListAsync();

        // mapa rápido IdDetalle -> Nota
        var mapa = cambios.ToDictionary(c => c.IdDetalle, c => c.Nota);

        foreach (var d in detalles)
        {
            if (mapa[d.IdDetalle] == null)
                throw new InvalidOperationException($"La nota para IdDetalle {d.IdDetalle} es obligatoria.");

            d.Nota = mapa[d.IdDetalle].Value;
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }
    public class GrupoOpcionVM
    {
        public int IdGrupo { get; set; }
        public string Display { get; set; } = ""; // lo que verá el usuario
    }

    // GET api/Notas/grupos
    [HttpGet("grupos")]
    public async Task<IActionResult> GetGrupos([FromQuery] int? idProfesor = null)
    {
      
        var q = from g in _db.Grupos.AsNoTracking()
                join m in _db.Materias.AsNoTracking() on g.IdMateria equals m.IdMateria
                select new { g, m };

        if (idProfesor.HasValue)
            q = q.Where(x => x.g.IdProfesor == idProfesor.Value);

        var lista = await q
            .OrderBy(x => x.m.Nombre).ThenBy(x => x.g.GrupoNumero)
            .Select(x => new GrupoOpcionVM
            {
                IdGrupo = x.g.IdGrupo,
                Display = $"{x.m.Nombre} - Grupo {x.g.GrupoNumero}"
            })
            .Distinct()
            .ToListAsync();

        return Ok(lista);
    }


}
