using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitosController : ControllerBase
    {
        private readonly IRequisitosService _service;

        public RequisitosController(IRequisitosService service)
        {
            _service = service;
        }

        // GET: api/Requisitos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllRequisitos();

            var result = items.Select(r => new
            {
                id_materia = r.IdMateria,
                id_requisito = r.IdRequisito,
                materia = r.Materia == null ? null : new
                {
                    r.Materia.IdMateria,
                    r.Materia.Codigo,
                    r.Materia.Nombre,
                    r.Materia.Creditos
                },
                requisito = r.MateriaRequisito == null ? null : new
                {
                    r.MateriaRequisito.IdMateria,
                    r.MateriaRequisito.Codigo,
                    r.MateriaRequisito.Nombre,
                    r.MateriaRequisito.Creditos
                }
            });

            return Ok(result);
        }

        // GET: api/Requisitos/{id_materia}/{id_requisito}
        [HttpGet("{id_materia:int}/{id_requisito:int}")]
        public async Task<IActionResult> GetByIds(int id_materia, int id_requisito)
        {
            var r = await _service.GetRequisitoByIds(id_materia, id_requisito);
            if (r == null) return NotFound();

            var result = new
            {
                id_materia = r.IdMateria,
                id_requisito = r.IdRequisito,
                materia = r.Materia == null ? null : new
                {
                    r.Materia.IdMateria,
                    r.Materia.Codigo,
                    r.Materia.Nombre,
                    r.Materia.Creditos
                },
                requisito = r.MateriaRequisito == null ? null : new
                {
                    r.MateriaRequisito.IdMateria,
                    r.MateriaRequisito.Codigo,
                    r.MateriaRequisito.Nombre,
                    r.MateriaRequisito.Creditos
                }
            };

            return Ok(result);
        }

        // GET: api/Requisitos/PorMateria/{id_materia}
        [HttpGet("PorMateria/{id_materia:int}")]
        public async Task<IActionResult> GetByMateria(int id_materia)
        {
            var items = await _service.GetRequisitosPorMateria(id_materia);
            var result = items.Select(r => new
            {
                id_materia = r.IdMateria,
                id_requisito = r.IdRequisito,
                requisito = r.MateriaRequisito == null ? null : new
                {
                    r.MateriaRequisito.IdMateria,
                    r.MateriaRequisito.Codigo,
                    r.MateriaRequisito.Nombre
                }
            });

            return Ok(result);
        }

        // GET: api/Requisitos/PorRequisito/{id_requisito}
        [HttpGet("PorRequisito/{id_requisito:int}")]
        public async Task<IActionResult> GetByRequisito(int id_requisito)
        {
            var items = await _service.GetRequisitosPorRequisito(id_requisito);
            var result = items.Select(r => new
            {
                id_materia = r.IdMateria,
                id_requisito = r.IdRequisito,
                materia = r.Materia == null ? null : new
                {
                    r.Materia.IdMateria,
                    r.Materia.Codigo,
                    r.Materia.Nombre
                }
            });

            return Ok(result);
        }

        // POST: api/Requisitos
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RequisitosCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (dto.id_materia <= 0 || dto.id_requisito <= 0)
                return BadRequest("id_materia e id_requisito deben ser > 0.");
            if (dto.id_materia == dto.id_requisito)
                return BadRequest("Una materia no puede ser requisito de sí misma.");

            var entity = new Requisito
            {
                IdMateria = dto.id_materia,
                IdRequisito = dto.id_requisito
            };

            await _service.AddRequisito(entity);

            // Traemos con Includes para devolverlo enriquecido
            var creado = await _service.GetRequisitoByIds(entity.IdMateria, entity.IdRequisito);
            var result = new
            {
                id_materia = creado?.IdMateria,
                id_requisito = creado?.IdRequisito,
                materia = creado?.Materia == null ? null : new
                {
                    creado.Materia.IdMateria,
                    creado.Materia.Codigo,
                    creado.Materia.Nombre,
                    creado.Materia.Creditos
                },
                requisito = creado?.MateriaRequisito == null ? null : new
                {
                    creado.MateriaRequisito.IdMateria,
                    creado.MateriaRequisito.Codigo,
                    creado.MateriaRequisito.Nombre,
                    creado.MateriaRequisito.Creditos
                }
            };

            return CreatedAtAction(nameof(GetByIds),
                new { id_materia = entity.IdMateria, id_requisito = entity.IdRequisito }, result);
        }

        // PUT: api/Requisitos/{id_materia}/{id_requisito}
        [HttpPut("{id_materia:int}/{id_requisito:int}")]
        public async Task<IActionResult> Update(int id_materia, int id_requisito, [FromBody] Requisito requisito)
        {
            if (requisito == null) return BadRequest("Datos inválidos.");
            if (id_materia != requisito.IdMateria || id_requisito != requisito.IdRequisito)
                return BadRequest("Los ids de la ruta no coinciden con el cuerpo.");

            if (requisito.IdMateria <= 0 || requisito.IdRequisito <= 0)
                return BadRequest("id_materia e id_requisito deben ser > 0.");
            if (requisito.IdMateria == requisito.IdRequisito)
                return BadRequest("Una materia no puede ser requisito de sí misma.");

            await _service.UpdateRequisito(requisito);
            return NoContent();
        }

        // DELETE: api/Requisitos/{id_materia}/{id_requisito}
        [HttpDelete("{id_materia:int}/{id_requisito:int}")]
        public async Task<IActionResult> Delete(int id_materia, int id_requisito)
        {
            await _service.DeleteRequisito(id_materia, id_requisito);
            return NoContent();
        }
    }
}
