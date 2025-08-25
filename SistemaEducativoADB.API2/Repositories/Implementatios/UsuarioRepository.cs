using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContext _context;

        public UsuarioRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Usuarios' with '_context.Set<Usuario>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Usuario DbSet.

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Set<Usuario>().FindAsync(id);
        }

        public async Task AddAsync(Usuario Usuario)
        {
            await _context.Set<Usuario>().AddAsync(Usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario Usuario)
        {
            _context.Set<Usuario>().Update(Usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Usuario = await GetByIdAsync(id);
            if (Usuario != null)
            {
                _context.Set<Usuario>().Remove(Usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
