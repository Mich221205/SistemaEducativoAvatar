using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly DbContext _context;

        public BitacoraRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Estudiantes' with '_context.Set<Estudiante>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Estudiante DbSet.

        public async Task<IEnumerable<Bitacora>> GetAllAsync()
        {
            return await _context.Set<Bitacora>().ToListAsync();
        }

        public async Task<Bitacora> GetByIdAsync(int id)
        {
            return await _context.Set<Bitacora>().FindAsync(id);
        }

        public async Task AddAsync(Bitacora bitacora)
        {
            await _context.Set<Bitacora>().AddAsync(bitacora);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bitacora bitacora)
        {
            _context.Set<Bitacora>().Update(bitacora);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bitacora = await GetByIdAsync(id);
            if (bitacora != null)
            {
                _context.Set<Bitacora>().Remove(bitacora);
                await _context.SaveChangesAsync();
            }
        }
    }
}
