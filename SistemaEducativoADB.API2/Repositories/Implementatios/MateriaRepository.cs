using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly DbContext _context;

        public MateriaRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Estudiantes' with '_context.Set<Estudiante>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Estudiante DbSet.

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Set<Materia>().ToListAsync();
        }

        public async Task<Materia> GetByIdAsync(int id)
        {
            return await _context.Set<Materia>().FindAsync(id);
        }

        public async Task AddAsync(Materia materia)
        {
            await _context.Set<Materia>().AddAsync(materia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Materia materia)
        {
            _context.Set<Materia>().Update(materia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var materia = await GetByIdAsync(id);
            if (materia != null)
            {
                _context.Set<Materia>().Remove(materia);
                await _context.SaveChangesAsync();
            }
        }

        Task<Materia> IMateriaRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
