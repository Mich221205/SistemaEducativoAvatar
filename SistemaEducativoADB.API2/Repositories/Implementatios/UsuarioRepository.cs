using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DBContext _context;

        public UsuarioRepository(DBContext context)
        {
            _context = context;
        }

        public async Task ActualizarDatosProfesorAsync(int idUsuario, ActualizarProfesorDto dto)
        {
            // Actualizar tabla USUARIOS
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario != null)
            {
                usuario.nombre = dto.Nombre;
                usuario.email = dto.Email;
            }

            // Actualizar tabla PROFESORES
            var profesor = await _context.Profesores.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);
            if (profesor != null)
            {
                profesor.Cedula = dto.Cedula;
                profesor.Telefono = dto.Telefono;
                profesor.CorreoPersonal = dto.CorreoPersonal;
            }

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Estudiante) 
                .ToListAsync();
        }


        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Profesor)   // 👈 traer también la relación
                .Include(u => u.Estudiante)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }


        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        //Login
        public async Task<Usuario?> LoginAsync(string email, string contrasena)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u =>
                    u.email == email &&
                    u.contrasena == contrasena &&
                    u.Estado == true);
        }

        public async Task CambiarEstadoAsync(int id, bool nuevoEstado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.Estado = nuevoEstado;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CambiarRolAsync(int id, int nuevoRol)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.IdRol = nuevoRol;
                await _context.SaveChangesAsync();
            }
        }
    }
}
