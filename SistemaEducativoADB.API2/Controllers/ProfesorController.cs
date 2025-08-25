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
    public class ProfesorsController : ControllerBase
    {
        private readonly IProfesorService _service;

        public ProfesorsController(IProfesorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Profesors = await _service.GetAllProfesors();
            return Ok(Profesors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Profesor = await _service.GetProfesorById(id);
            if (Profesor == null) return NotFound();
            return Ok(Profesor);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProfesor([FromBody] ProfesorCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var profesor = new Profesor
            {
                IdUsuario = dto.IdUsuario,      
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                CorreoPersonal = dto.CorreoPersonal
            };

            await _service.AddProfesor(profesor);

            return CreatedAtAction(nameof(GetById), new { id = profesor.IdProfesor }, profesor);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Profesor Profesor)
        {
            if (id != Profesor.IdProfesor) return BadRequest();
            await _service.UpdateProfesor(Profesor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProfesor(id);
            return NoContent();
        }
    }
}
