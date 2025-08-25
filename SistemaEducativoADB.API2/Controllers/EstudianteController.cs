using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _service;

        public EstudiantesController(IEstudianteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _service.GetAllEstudiantes();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estudiante = await _service.GetEstudianteById(id);
            if (estudiante == null) return NotFound();
            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> CrearEstudiante([FromBody] EstudianteCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var estudiante = new Estudiante
            {
                IdUsuario = dto.IdUsuario,
                Carnet = dto.carnet,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                IdCarrera = dto.IdCarrera
            };

            await _service.AddEstudiante(estudiante);

            return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudiante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Estudiante estudiante)
        {
            if (id != estudiante.IdEstudiante) return BadRequest();
            await _service.UpdateEstudiante(estudiante);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteEstudiante(id);
            return NoContent();
        }
    }
}
