using Microsoft.AspNetCore.Mvc;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _service;

        public RolesController(IRolesService service)
        {
            _service = service;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _service.GetAllRoles();

            var result = roles.Select(r => new {
                r.IdRol,
                r.NombreRol,
                usuarios = r.Usuarios.Select(u => new { u.IdUsuario, u.nombre })
            });

            return Ok(result);
        }

        // GET: api/Roles
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _service.GetRolById(id);
            if (r == null) return NotFound();

            var result = new
            {
                idRol = r.IdRol,
                nombreRol = r.NombreRol,
                usuarios = (r.Usuarios ?? Array.Empty<Usuario>())
                           .Select(u => new { u.IdUsuario, u.nombre })
            };

            return Ok(result);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] RolesCreateDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            if (string.IsNullOrWhiteSpace(dto.nombre_rol))
                return BadRequest("El nombre del rol es obligatorio.");

            var rol = new Rol
            {
                NombreRol = dto.nombre_rol.Trim()
            };

            await _service.AddRol(rol);

            return CreatedAtAction(nameof(GetById), new { id = rol.IdRol }, rol);
        }

        // PUT: api/Roles
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rol rol)
        {
            if (rol == null) return BadRequest("Datos inválidos.");
            if (id != rol.IdRol) return BadRequest("El id de la ruta no coincide con el del cuerpo.");
            if (string.IsNullOrWhiteSpace(rol.NombreRol))
                return BadRequest("El nombre del rol es obligatorio.");

            rol.NombreRol = rol.NombreRol.Trim();

            await _service.UpdateRol(rol);
            return NoContent();
        }

        // DELETE: api/Roles
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRol(id);
            return NoContent();
        }
    }
}
