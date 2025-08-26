using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Plan_EstudioController : ControllerBase
    {
        private readonly IPlan_EstudioService _service;

        public Plan_EstudioController(IPlan_EstudioService service)
        {
            _service = service;
        }

        // GET: api/Plan_Estudio
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllPlanes();
            var result = data.Select(p => new
            {
                p.id_plan,
                p.id_carrera,
                p.anio_inicio,
                carrera = p.Carrera == null ? null : new
                {
                    p.Carrera.IdCarrera,
                    p.Carrera.NombreCarrera
                },
                materias = p.Materias?.Select(m => new
                {
                    m.IdMateria,
                    m.Codigo,
                    m.Nombre,
                    m.Creditos
                })
            });

            return Ok(result);
        }

        // GET: api/Plan_Estudio
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _service.GetPlanById(id);
            if (p == null) return NotFound();

            var result = new
            {
                p.id_plan,
                p.id_carrera,
                p.anio_inicio,
                carrera = p.Carrera == null ? null : new
                {
                    p.Carrera.IdCarrera,
                    p.Carrera.NombreCarrera
                },
                materias = p.Materias?.Select(m => new
                {
                    m.IdMateria,
                    m.Codigo,
                    m.Nombre,
                    m.Creditos
                })
            };

            return Ok(result);
        }

        // GET: api/Plan_Estudio/PorCarrera
        [HttpGet("PorCarrera/{id_carrera}")]
        public async Task<IActionResult> GetPorCarrera(int id_carrera)
        {
            var planes = await _service.GetPlanesPorCarrera(id_carrera);
            return Ok(planes);
        }

        // GET: api/Plan_Estudio/PorCarreraYAnio
        [HttpGet("PorCarreraYAnio")]
        public async Task<IActionResult> GetPorCarreraYAnio([FromQuery] int idCarrera, [FromQuery] int anio)
        {
            if (idCarrera <= 0 || anio <= 0) return BadRequest("Parámetros inválidos.");
            var plan = await _service.GetPlanPorCarreraYAnio(idCarrera, anio);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        // POST: api/Plan_Estudio
        [HttpPost]
        public async Task<IActionResult> CrearPlan([FromBody] Plan_EstudioCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (dto.id_carrera <= 0) return BadRequest("IdCarrera inválido.");
            if (dto.anio_inicio <= 0) return BadRequest("AnioInicio inválido.");

            var plan = new Plan_Estudio
            {
                id_carrera = dto.id_carrera,
                anio_inicio = dto.anio_inicio
            };

            // 1) Guardar
            await _service.AddPlan(plan);

            // 2) Recargar con navegación (Carrera y Materias) usando GetById que ya hace Include()
            var creado = await _service.GetPlanById(plan.id_plan);
            if (creado == null) return Problem("No se pudo recargar el plan creado.");

            // 3) Proyección “bonita” para la respuesta
            var result = new
            {
                creado.id_plan,
                creado.id_carrera,
                creado.anio_inicio,
                carrera = creado.Carrera == null ? null : new
                {
                    creado.Carrera.IdCarrera,
                    creado.Carrera.NombreCarrera
                },
                // si no tiene materias asociadas aún, devolvemos arreglo vacío en vez de null
                materias = (creado.Materias ?? new List<Materia>())
                    .Select(m => new { m.IdMateria, m.Codigo, m.Nombre, m.Creditos })
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.id_plan }, result);
        }

        // PUT: api/Plan_Estudio
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Plan_Estudio plan)
        {
            if (plan == null) return BadRequest("Datos inválidos.");
            if (id != plan.id_plan) return BadRequest("El id de la ruta no coincide con el del cuerpo.");
            if (plan.id_carrera <= 0) return BadRequest("IdCarrera inválido.");
            if (plan.anio_inicio <= 0) return BadRequest("AnioInicio inválido.");

            await _service.UpdatePlan(plan);
            return NoContent();
        }

        // DELETE: api/Plan_Estudio
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePlan(id);
            return NoContent();
        }
    }
}
