using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services;
using SistemaEducativoADB.API2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;    

namespace SistemaEducativoADB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly DBContext _db;
        public EstudiantesController(DBContext db) => _db = db;
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

        public class CambiarCarreraDto { public int idCarrera { get; set; } }

        // PUT /api/estudiantes/usuario/123/carrera
        [HttpPut("usuario/{idUsuario:int}/carrera")]
        public async Task<IActionResult> CambiarCarreraPorUsuario(int idUsuario, [FromBody] CambiarCarreraDto dto)
        {
            if (dto == null || dto.idCarrera <= 0)
                return BadRequest("Debe indicar 'idCarrera'.");

            // Buscamos al estudiante por su usuario
            var est = await _db.Estudiantes.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);
            if (est == null) return NotFound("Estudiante no encontrado.");

            // validar que la carrera exista
            var existeCarrera = await _db.Carreras.AnyAsync(c => c.IdCarrera == dto.idCarrera);
            if (!existeCarrera) return NotFound("La carrera indicada no existe.");

            est.IdCarrera = dto.idCarrera;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
