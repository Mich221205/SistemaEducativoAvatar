using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly DbContext _context;

        public EstudianteRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Estudiantes' with '_context.Set<Estudiante>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Estudiante DbSet.

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Set<Estudiante>().ToListAsync();
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            return await _context.Set<Estudiante>().FindAsync(id);
        }

        public async Task AddAsync(Estudiante estudiante)
        {
            await _context.Set<Estudiante>().AddAsync(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Set<Estudiante>().Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await GetByIdAsync(id);
            if (estudiante != null)
            {
                _context.Set<Estudiante>().Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }
    }
}
