using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API.Models.DTOs;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _service;

        public MateriaController(IMateriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Materias = await _service.GetAllMaterias();
            return Ok(Materias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Materia = await _service.GetMateriaById(id);
            if (Materia == null) return NotFound();
            return Ok(Materia);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMateria([FromBody] MateriaCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.nombre))
                return BadRequest("El nombre de la materia es obligatorio.");

            var materia = new Materia
            {
                Nombre = dto.nombre
            };

            await _service.AddMateria(materia);

            return CreatedAtAction(nameof(GetById), new { id = materia.IdMateria }, materia);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Materia materia)
        {
            if (id != materia.IdMateria) return BadRequest();
            await _service.UpdateMateria(materia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMateria(id);
            return NoContent();
        }
    }
}
