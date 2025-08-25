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
    public class CarrerasController : ControllerBase
    {
        private readonly ICarreraService _service;

        public CarrerasController(ICarreraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Carreras = await _service.GetAllCarreras();
            return Ok(Carreras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Carrera = await _service.GetCarreraById(id);
            if (Carrera == null) return NotFound();
            return Ok(Carrera);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCarrera([FromBody] CarreraCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.NombreCarrera))
                return BadRequest("El nombre de la carrera es obligatorio.");

            var carrera = new Carrera
            {
                NombreCarrera = dto.NombreCarrera
            };

            await _service.AddCarrera(carrera);

            return CreatedAtAction(nameof(GetById), new { id = carrera.IdCarrera }, carrera);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Carrera Carrera)
        {
            if (id != Carrera.IdCarrera) return BadRequest();
            await _service.UpdateCarrera(Carrera);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCarrera(id);
            return NoContent();
        }
    }
}
