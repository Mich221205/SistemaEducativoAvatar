using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DBContext _context;

        public RolesRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Set<Rol>()
                                 .AsNoTracking()
                                 .Include(r => r.Usuarios)
                                 .ToListAsync();
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _context.Set<Rol>()
                .AsNoTracking()
                .Include(r => r.Usuarios)
                .FirstOrDefaultAsync(r => r.IdRol == id || r.IdRol == id);
        }

        public async Task AddAsync(Rol rol)
        {
            rol.NombreRol = (rol.NombreRol ?? string.Empty).Trim();

            await _context.Set<Rol>().AddAsync(rol);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rol rol)
        {
            rol.NombreRol = (rol.NombreRol ?? string.Empty).Trim();

            _context.Set<Rol>().Update(rol);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Rol>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
