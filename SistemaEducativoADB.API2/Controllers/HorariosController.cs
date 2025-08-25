using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IHorariosService _service;

        public HorariosController(IHorariosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var horarios = await _service.GetAllHorarios();

            var result = horarios.Select(h => new
            {
                idHorario = h.IdHorario,
                idGrupo = h.IdGrupo,
                diaSemana = h.DiaSemana,
                horaInicio = h.HoraInicio,
                horaFin = h.HoraFin,
                grupo = h.Grupo == null ? null : new
                {
                    idGrupo = h.Grupo.IdGrupo,
                    idMateria = h.Grupo.IdMateria,
                    idProfesor = h.Grupo.IdProfesor,
                    grupoNumero = h.Grupo.GrupoNumero,
                    aula = h.Grupo.Aula,
                    cupoMax = h.Grupo.CupoMax
                }
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var h = await _service.GetHorarioById(id);
            if (h == null) return NotFound();

            var result = new
            {
                idHorario = h.IdHorario,
                idGrupo = h.IdGrupo,
                diaSemana = h.DiaSemana,
                horaInicio = h.HoraInicio,
                horaFin = h.HoraFin,
                grupo = h.Grupo == null ? null : new
                {
                    idGrupo = h.Grupo.IdGrupo,
                    idMateria = h.Grupo.IdMateria,
                    idProfesor = h.Grupo.IdProfesor,
                    grupoNumero = h.Grupo.GrupoNumero,
                    aula = h.Grupo.Aula,
                    cupoMax = h.Grupo.CupoMax
                }
            };

            return Ok(result);
        }

        [HttpGet("PorGrupo/{id_grupo}")]
        public async Task<IActionResult> GetByGrupo(int id_grupo)
        {
            var horarios = await _service.GetByGrupo(id_grupo);

            var result = horarios.Select(h => new
            {
                idHorario = h.IdHorario,
                idGrupo = h.IdGrupo,
                diaSemana = h.DiaSemana,
                horaInicio = h.HoraInicio,
                horaFin = h.HoraFin,
                grupo = h.Grupo == null ? null : new
                {
                    idGrupo = h.Grupo.IdGrupo,
                    idMateria = h.Grupo.IdMateria,
                    idProfesor = h.Grupo.IdProfesor,
                    grupoNumero = h.Grupo.GrupoNumero,
                    aula = h.Grupo.Aula,
                    cupoMax = h.Grupo.CupoMax
                }
            });

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CrearHorario([FromBody] HorariosCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (dto.hora_inicio >= dto.hora_fin) return BadRequest("La hora de inicio debe ser menor que la hora fin.");

            var dia = (dto.dia_semana ?? "").Trim();


            if (await _service.ExistsOverlap(dto.id_grupo, dia, dto.hora_inicio, dto.hora_fin))
                return Conflict("Ya existe un horario idéntico para ese grupo y día.");

            // 2) Solapamiento
            if (await _service.ExistsOverlap(dto.id_grupo, dia, dto.hora_inicio, dto.hora_fin))
                return Conflict("El horario se solapa con otro existente para ese grupo y día.");

            var horario = new Horario
            {
                IdGrupo = dto.id_grupo,
                DiaSemana = dia,
                HoraInicio = dto.hora_inicio,
                HoraFin = dto.hora_fin
            };

            await _service.AddHorario(horario);

            // Recargar con Include(h => h.Grupo) para no devolver grupo = null
            var creado = await _service.GetHorarioById(horario.IdHorario);

            var result = new
            {
                idHorario = creado!.IdHorario,
                idGrupo = creado.IdGrupo,
                diaSemana = creado.DiaSemana,
                horaInicio = creado.HoraInicio,
                horaFin = creado.HoraFin,
                grupo = creado.Grupo == null ? null : new
                {
                    idGrupo = creado.Grupo.IdGrupo,
                    idMateria = creado.Grupo.IdMateria,
                    idProfesor = creado.Grupo.IdProfesor,
                    grupoNumero = creado.Grupo.GrupoNumero,
                    aula = creado.Grupo.Aula,
                    cupoMax = creado.Grupo.CupoMax
                }
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.IdHorario }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HorarioPutBody body)
        {
            if (body == null || id != body.idHorario)
                return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            if (body.horaInicio >= body.horaFin)
                return BadRequest("La hora de inicio debe ser menor que la hora fin.");

            var entity = await _service.GetHorarioById(id);
            if (entity == null) return NotFound();

            // Actualiza solo lo necesario
            entity.IdGrupo = body.idGrupo;
            entity.DiaSemana = body.diaSemana?.Trim() ?? "";
            entity.HoraInicio = body.horaInicio;
            entity.HoraFin = body.horaFin;

            await _service.UpdateHorario(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteHorario(id);
            return NoContent();
        }

        public class HorarioPutBody
        {
            public int idHorario { get; set; }
            public int idGrupo { get; set; }
            public string diaSemana { get; set; } = "";
            public TimeSpan horaInicio { get; set; }
            public TimeSpan horaFin { get; set; }
            public GrupoMiniPut? grupo { get; set; }
        }

        public class GrupoMiniPut
        {
            public int idGrupo { get; set; }
            public int idMateria { get; set; }
            public int idProfesor { get; set; }
            public string grupoNumero { get; set; } = "";
            public string aula { get; set; } = "";
            public int cupoMax { get; set; }
        }
    }
}
