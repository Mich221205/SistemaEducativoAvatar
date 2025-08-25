using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalle_MatriculasController : ControllerBase
    {
        private readonly IDetalle_MatriculaService _service;

        public Detalle_MatriculasController(IDetalle_MatriculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var detalle_matriculas = await _service.GetAllDetalle_Matriculas();
            return Ok(detalle_matriculas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detalle_matricula = await _service.GetDetalle_MatriculaById(id);
            if (detalle_matricula == null) return NotFound();
            return Ok(detalle_matricula);
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalle_Matricula([FromBody] Detalle_MatriculaCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var detalle_matricula = new Detalle_Matricula
            {
                IdMatricula = dto.IdMatricula,
                IdGrupo = dto.IdGrupo,
                Nota = dto.Nota,
                Condicion = dto.Condicion
            };

            await _service.AddDetalle_Matricula(detalle_matricula);

            return CreatedAtAction(nameof(GetById), new { id = detalle_matricula.IdMatricula }, detalle_matricula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Detalle_Matricula detalle_matricula)
        {
            if (id != detalle_matricula.IdMatricula) return BadRequest();
            await _service.UpdateDetalle_Matricula(detalle_matricula);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDetalle_Matricula(id);
            return NoContent();
        }
    }
}
