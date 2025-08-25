using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _service;

        public MatriculasController(IMatriculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matriculas = await _service.GetAllMatriculas();
            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var matricula = await _service.GetMatriculaById(id);
            if (matricula == null) return NotFound();
            return Ok(matricula);
        }

        [HttpGet("Estudiante/{id_estudiante}")]
        public async Task<IActionResult> GetByEstudiante(int id_estudiante)
        {
            var matriculas = await _service.GetByEstudiante(id_estudiante);
            return Ok(matriculas);
        }

        [HttpGet("Periodo/{id_periodo}")]
        public async Task<IActionResult> GetByPeriodo(int id_periodo)
        {
            var matriculas = await _service.GetByPeriodo(id_periodo);
            return Ok(matriculas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMatricula([FromBody] MatriculaCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Los datos de la matrícula son obligatorios.");

            var exists = await _service.ExistsForEstudiantePeriodo(dto.id_estudiante, dto.id_periodo);
            if (exists)
                return Conflict("El estudiante ya tiene una matrícula en este período.");

            var matricula = new Matricula
            {
                id_estudiante = dto.id_estudiante,
                id_periodo = dto.id_periodo,
                estado = string.IsNullOrWhiteSpace(dto.estado) ? "Pendiente" : dto.estado
            };

            await _service.AddMatricula(matricula);

            return CreatedAtAction(nameof(GetById), new { id = matricula.id_matricula }, matricula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Matricula matricula)
        {
            if (id != matricula.id_matricula)
                return BadRequest("El id no coincide con la matrícula.");

            await _service.UpdateMatricula(matricula);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMatricula(id);
            return NoContent();
        }
    }
}
