using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;

namespace SistemaEducativoADB.API2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly DBContext _db;
        public PerfilController(DBContext db) => _db = db;

        // DTOs (puedes moverlos a API2.Models.DTOs si prefieres)
        public class PerfilDto
        {
            public int IdUsuario { get; set; }
            public int IdEstudiante { get; set; }
            public int? IdCarrera { get; set; }   // ← nullable para evitar CS0266
            public string Carne { get; set; } = "";
            public string Nombre { get; set; } = "";
            public string Email { get; set; } = "";
            public string Telefono { get; set; } = "";
            public string Direccion { get; set; } = "";
        }

        public class PerfilUpdateDto
        {
            public string Email { get; set; } = "";
            public string Telefono { get; set; } = "";
            public string Direccion { get; set; } = "";
        }

        // GET: /api/perfil/usuario/123
        [HttpGet("usuario/{usuarioId:int}")]
        public async Task<ActionResult<PerfilDto>> GetPorUsuario(int usuarioId)
        {
            var dto = await (from e in _db.Estudiantes.AsNoTracking()   // ← Estudiantes
                             join u in _db.Usuarios.AsNoTracking()
                               on e.IdUsuario equals u.IdUsuario
                             where u.IdUsuario == usuarioId
                             select new PerfilDto
                             {
                                 IdUsuario = u.IdUsuario,
                                 IdEstudiante = e.IdEstudiante,
                                 IdCarrera = e.IdCarrera,          // int? → ok
                                 Carne = e.Carnet,
                                 Nombre = u.nombre,
                                 Email = u.email,
                                 Telefono = e.Telefono ?? "",
                                 Direccion = e.Direccion ?? ""
                             }).SingleOrDefaultAsync();

            if (dto is null) return NotFound();
            return Ok(dto);
        }

        // PUT: /api/perfil/usuario/123
        [HttpPut("usuario/{usuarioId:int}")]
        public async Task<IActionResult> PutPorUsuario(int usuarioId, [FromBody] PerfilUpdateDto body)
        {
            if (body is null) return BadRequest("Datos inválidos.");

            var u = await _db.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
            var e = await _db.Estudiantes.FirstOrDefaultAsync(x => x.IdUsuario == usuarioId);
            if (u is null || e is null) return NotFound();

            u.email = (body.Email ?? "").Trim();
            e.Telefono = (body.Telefono ?? "").Trim();
            e.Direccion = (body.Direccion ?? "").Trim();

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
