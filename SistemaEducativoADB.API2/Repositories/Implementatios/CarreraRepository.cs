using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly DbContext _context;

        public CarreraRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Carreras' with '_context.Set<Carrera>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Carrera DbSet.

        public async Task<IEnumerable<Carrera>> GetAllAsync()
        {
            return await _context.Set<Carrera>().ToListAsync();
        }

        public async Task<Carrera> GetByIdAsync(int id)
        {
            return await _context.Set<Carrera>().FindAsync(id);
        }

        public async Task AddAsync(Carrera Carrera)
        {
            await _context.Set<Carrera>().AddAsync(Carrera);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Carrera Carrera)
        {
            _context.Set<Carrera>().Update(Carrera);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Carrera = await GetByIdAsync(id);
            if (Carrera != null)
            {
                _context.Set<Carrera>().Remove(Carrera);
                await _context.SaveChangesAsync();
            }
        }
    }
}
