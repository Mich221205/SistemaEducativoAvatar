using Microsoft.AspNetCore.Mvc;
using SistemaEducativo.Frontend.Models;
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

        // GET: api/Usuario/estudiantes/activos
        [HttpGet("estudiantes/activos")]
        public async Task<IActionResult> GetEstudiantesActivos()
        {
            var usuarios = await _service.GetAllUsuarios();

            var result = usuarios
                .Where(u => true && u.IdRol == 3) // 👈 solo rol estudiante
                .Select(u => new EstudianteDto
                {
                    Carnet = u.Estudiante != null ? u.Estudiante.Carnet : "",
                    Nombre = u.nombre,
                    FechaIngreso = u.FechaCreacion,
                    Email = u.email,
                    Estado = u.Estado
                });

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _service.GetAllUsuarios();

            var result = usuarios.Select(u => new {
                u.IdUsuario,
                u.nombre,
                u.email,
                u.Estado,
                u.FechaCreacion,
                IdRol = u.IdRol,
                RolNombre = u.Rol != null ? u.Rol.NombreRol : string.Empty
            });

            return Ok(result);
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

            var usuario = new Usuario
            {
                nombre = dto.nombre,
                email = dto.email,
                contrasena = dto.contrasena,
                Estado = false, //se crea inactivo por defecto para que el admin lo active
                FechaCreacion = DateTime.Now,
                IdRol = 35 // Asignar el rol de "no definido" por defecto
            };

            await _service.AddUsuario(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            Console.WriteLine($"Email: {dto.email}, Pass: {dto.contrasena}");

            var usuario = await _service.Login(dto.email, dto.contrasena);
            if (usuario == null)
            {
                Console.WriteLine("❌ No se encontró usuario.");
                return Unauthorized("Credenciales inválidas");
            }

            Console.WriteLine($"✅ Usuario encontrado: {usuario.nombre}, Rol: {usuario.IdRol}");

            var result = new
            {
                IdUsuario = usuario.IdUsuario,
                nombre = usuario.nombre,
                email = usuario.email,
                id_rol = usuario.IdRol,
                RolNombre = usuario.Rol.NombreRol
            };

            return Ok(result);
        }

        [HttpGet("inactivos")]
        public async Task<IActionResult> GetInactivos()
        {
            var usuarios = await _service.GetAllUsuarios();

            var result = usuarios
                .Where(u => !u.Estado)
                .Select(u => new {
                    u.IdUsuario,
                    u.nombre,
                    u.email,
                    u.Estado,
                    u.FechaCreacion,
                    IdRol = u.IdRol,
                    RolNombre = u.Rol != null ? u.Rol.NombreRol : "No definido aún"
                });

            return Ok(result);
        }

        // Activar/Desactivar usuario
        [HttpPut("cambiar-estado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] bool nuevoEstado)
        {
            await _service.CambiarEstadoAsync(id, nuevoEstado);
            return Ok(new { IdUsuario = id, Estado = nuevoEstado });
        }

        [HttpPut("cambiar-rol/{id}")]
        public async Task<IActionResult> CambiarRol(int id, [FromBody] int nuevoRol)
        {
            await _service.CambiarRolAsync(id, nuevoRol);
            return Ok(new { IdUsuario = id, IdRol = nuevoRol });
        }


    }
}
