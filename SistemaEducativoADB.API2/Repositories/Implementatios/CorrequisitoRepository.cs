using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class CorrequisitoRepository : ICorrequisitoRepository
    {
        private readonly DbContext _context;

        public CorrequisitoRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Correquisitos' with '_context.Set<Correquisito>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Correquisito DbSet.

        public async Task<IEnumerable<Correquisito>> GetAllAsync()
        {
            return await _context.Set<Correquisito>().ToListAsync();
        }

        public async Task<Correquisito> GetByIdAsync(int id)
        {
            return await _context.Set<Correquisito>().FindAsync(id);
        }

        public async Task AddAsync(Correquisito correquisito)
        {
            await _context.Set<Correquisito>().AddAsync(correquisito);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Correquisito correquisito)
        {
            _context.Set<Correquisito>().Update(correquisito);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var correquisito = await GetByIdAsync(id);
            if (correquisito != null)
            {
                _context.Set<Correquisito>().Remove(correquisito);
                await _context.SaveChangesAsync();
            }
        }
    }
}
