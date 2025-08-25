using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrequisitosController : ControllerBase
    {
        private readonly ICorrequisitoService _service;

        public CorrequisitosController(ICorrequisitoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var correquisitos = await _service.GetAllCorrequisitos();
            return Ok(correquisitos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var correquisito = await _service.GetCorrequisitoById(id);
            if (correquisito == null) return NotFound();
            return Ok(correquisito);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCorrequisito([FromBody] CorrequisitoCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var correquisito = new Correquisito
            {
                IdMateria = dto.IdMateria,
                IdCorrequisito = dto.IdCorrequisito
            };

            await _service.AddCorrequisito(correquisito);

            return CreatedAtAction(nameof(GetById), new { idMateria = correquisito.IdMateria, idCorrequisito = correquisito.IdCorrequisito }, correquisito);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Correquisito correquisito)
        {
            if (id != correquisito.IdCorrequisito) return BadRequest();
            await _service.UpdateCorrequisito(correquisito);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCorrequisito(id);
            return NoContent();
        }
    }
}
