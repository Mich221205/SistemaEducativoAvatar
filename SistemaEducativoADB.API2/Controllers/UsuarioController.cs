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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Usuarios = await _service.GetAllUsuarios();
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Usuarios = await _service.GetUsuarioById(id);
            if (Usuarios == null) return NotFound();
            return Ok(Usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos");

            var Usuarios = new Usuario
            {
                IdUsuario = dto.IdUsuario,
                nombre = dto.nombre,
                email = dto.email,
                contrasena = dto.contrasena,
                FechaCreacion = dto.FechaCreacion
            };
       

            await _service.AddUsuario(Usuarios);

            return CreatedAtAction(nameof(GetById), new { id = Usuarios.IdUsuario }, Usuarios);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.IdUsuario) return BadRequest();
            await _service.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteUsuario(id);
            return NoContent();
        }
    }
}
