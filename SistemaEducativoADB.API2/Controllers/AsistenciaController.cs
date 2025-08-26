using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _service;

        public AsistenciaController(IAsistenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var asistencias = await _service.GetAllAsistencia();
            return Ok(asistencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var asistencia = await _service.GetAsistenciaById(id);
            if (asistencia == null) return NotFound();
            return Ok(asistencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Asistencia asistencia)
        {
            await _service.AddAsistencia(asistencia);

            
            return Ok(asistencia);

    
            return Ok(new { success = true, message = "Asistencia creada correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Asistencia asistencia)
        {
            if (id == 0)
            {
                
                await _service.AddAsistencia(asistencia);
                return Ok(asistencia);
            }

            if (id != asistencia.IdAsistencia) return BadRequest();
            await _service.UpdateAsistencia(asistencia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsistencia(id);
            return NoContent();
        }
    }
}
