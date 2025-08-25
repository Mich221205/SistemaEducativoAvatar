using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private readonly IGruposService _service;

        public GruposController(IGruposService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var grupos = await _service.GetAllGrupos();

            var result = grupos.Select(g => new
            {
                idGrupo = g.IdGrupo,
                idMateria = g.IdMateria,
                idProfesor = g.IdProfesor,
                grupoNumero = g.GrupoNumero,
                aula = g.Aula,
                cupoMax = g.CupoMax,
                materia = g.Materia == null ? null : new
                {
                    idMateria = g.Materia.IdMateria,
                    codigo = g.Materia.Codigo,
                    nombre = g.Materia.Nombre,
                    creditos = g.Materia.Creditos,
                    idPlan = g.Materia.IdPlan
                }
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var g = await _service.GetGrupoById(id);
            if (g == null) return NotFound();

            var result = new
            {
                idGrupo = g.IdGrupo,
                idMateria = g.IdMateria,
                idProfesor = g.IdProfesor,
                grupoNumero = g.GrupoNumero,
                aula = g.Aula,
                cupoMax = g.CupoMax,
                materia = g.Materia == null ? null : new
                {
                    idMateria = g.Materia.IdMateria,
                    codigo = g.Materia.Codigo,
                    nombre = g.Materia.Nombre,
                    creditos = g.Materia.Creditos,
                    idPlan = g.Materia.IdPlan
                }
            };

            return Ok(result);
        }

        [HttpGet("PorMateria/{id_materia}")]
        public async Task<IActionResult> GetByMateria(int id_materia)
        {
            var grupos = await _service.GetByMateria(id_materia);

            var result = grupos.Select(g => new
            {
                idGrupo = g.IdGrupo,
                idMateria = g.IdMateria,
                idProfesor = g.IdProfesor,
                grupoNumero = g.GrupoNumero,
                aula = g.Aula,
                cupoMax = g.CupoMax,
                materia = g.Materia == null ? null : new
                {
                    idMateria = g.Materia.IdMateria,
                    codigo = g.Materia.Codigo,
                    nombre = g.Materia.Nombre,
                    creditos = g.Materia.Creditos,
                    idPlan = g.Materia.IdPlan
                }
            });

            return Ok(result);
        }

        [HttpGet("PorProfesor/{id_profesor}")]
        public async Task<IActionResult> GetByProfesor(int id_profesor)
        {
            var grupos = await _service.GetByProfesor(id_profesor);

            var result = grupos.Select(g => new
            {
                idGrupo = g.IdGrupo,
                idMateria = g.IdMateria,
                idProfesor = g.IdProfesor,
                grupoNumero = g.GrupoNumero,
                aula = g.Aula,
                cupoMax = g.CupoMax,
                materia = g.Materia == null ? null : new
                {
                    idMateria = g.Materia.IdMateria,
                    codigo = g.Materia.Codigo,
                    nombre = g.Materia.Nombre,
                    creditos = g.Materia.Creditos,
                    idPlan = g.Materia.IdPlan
                }
            });

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CrearGrupo([FromBody] GruposCreateDto dto)
        {
            if (dto == null) return BadRequest("Los datos del grupo son obligatorios.");

            if (dto.id_materia <= 0) return BadRequest("id_materia inválido.");
            if (dto.id_profesor <= 0) return BadRequest("id_profesor inválido.");
            if (string.IsNullOrWhiteSpace(dto.grupo_numero)) return BadRequest("grupo_numero es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.aula)) return BadRequest("aula es obligatoria.");
            if (dto.cupo_max <= 0) return BadRequest("cupo_max debe ser mayor a 0.");

            var exists = await _service.ExistsForMateriaNumero(dto.id_materia, dto.grupo_numero);
            if (exists) return Conflict("Ya existe un grupo con ese número para la materia indicada.");

            var grupo = new Grupo
            {
                IdMateria = dto.id_materia,
                IdProfesor = dto.id_profesor,
                GrupoNumero = dto.grupo_numero.Trim(),
                Aula = dto.aula.Trim(),
                CupoMax = dto.cupo_max
            };

            await _service.AddGrupo(grupo);
            return CreatedAtAction(nameof(GetById), new { id = grupo.IdGrupo }, grupo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GrupoPutBody body)
        {
            if (body == null || id != body.idGrupo)
                return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            if (body.idMateria <= 0 || body.idProfesor <= 0 ||
                string.IsNullOrWhiteSpace(body.grupoNumero) ||
                string.IsNullOrWhiteSpace(body.aula) ||
                body.cupoMax <= 0)
                return BadRequest("Datos inválidos.");

            var grupo = await _service.GetGrupoById(id);
            if (grupo == null) return NotFound();

            // Actualiza solo los campos permitidos
            grupo.IdMateria = body.idMateria;
            grupo.IdProfesor = body.idProfesor;
            grupo.GrupoNumero = body.grupoNumero.Trim();
            grupo.Aula = body.aula.Trim();
            grupo.CupoMax = body.cupoMax;

            await _service.UpdateGrupo(grupo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteGrupo(id);
            return NoContent();
        }
        public class GrupoPutBody
        {
            public int idGrupo { get; set; }
            public int idMateria { get; set; }
            public int idProfesor { get; set; }
            public string grupoNumero { get; set; } = "";
            public string aula { get; set; } = "";
            public int cupoMax { get; set; }
            public MateriaMiniPut? materia { get; set; }
        }

        public class MateriaMiniPut
        {
            public int idMateria { get; set; }
            public string codigo { get; set; } = "";
            public string nombre { get; set; } = "";
            public int creditos { get; set; }
            public int? idPlan { get; set; }
        }
    }
}
