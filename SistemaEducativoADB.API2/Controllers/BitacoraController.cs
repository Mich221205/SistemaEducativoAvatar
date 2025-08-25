using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacorasController : ControllerBase
    {
        private readonly IBitacoraService _service;

        public BitacorasController(IBitacoraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bitacoras = await _service.GetAllBitacoras();
            return Ok(bitacoras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bitacora = await _service.GetBitacoraById(id);
            if (bitacora == null) return NotFound();
            return Ok(bitacora);
        }

        [HttpPost]
        public async Task<IActionResult> CrearBitacora([FromBody] BitacoraCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var bitacora = new Bitacora
            {
                IdUsuario = dto.IdUsuario,
                Accion = dto.Accion,
                Ip = dto.Ip,
                FechaHora = DateTime.Now, // opcional, ya que la DB lo establece por defecto
                Usuario = new Usuario() // Fix: set required Usuario property to avoid CS9035
            };

            await _service.AddBitacora(bitacora);

            return CreatedAtAction(nameof(GetById), new { id = bitacora.IdLog }, bitacora);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteBitacora(id);
            return NoContent();
        }
    }
}
