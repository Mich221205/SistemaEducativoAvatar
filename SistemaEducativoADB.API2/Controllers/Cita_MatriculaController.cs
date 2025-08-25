using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cita_MatriculasController : ControllerBase
    {
        private readonly ICita_MatriculaService _service;

        public Cita_MatriculasController(ICita_MatriculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cita_matriculas = await _service.GetAllCita_Matriculas();
            return Ok(cita_matriculas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cita_matricula = await _service.GetCita_MatriculaById(id);
            if (cita_matricula == null) return NotFound();
            return Ok(cita_matricula);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCitaMatricula([FromBody] CitaMatriculaCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var citaMatricula = new Cita_Matricula
            {
                IdEstudiante = dto.IdEstudiante,
                IdPeriodo = dto.IdPeriodo,
                FechaHora = dto.FechaHora
            };

            await _service.AddCita_Matricula(citaMatricula);

            return CreatedAtAction(nameof(GetById), new { id = citaMatricula.IdCita }, citaMatricula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cita_Matricula cita_matricula)
        {
            if (id != cita_matricula.IdCita) return BadRequest();
            await _service.UpdateCita_Matricula(cita_matricula);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCita_Matricula(id);
            return NoContent();
        }
    }
}
