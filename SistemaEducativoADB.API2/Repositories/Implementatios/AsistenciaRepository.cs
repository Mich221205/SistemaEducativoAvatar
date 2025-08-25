using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly DbContext _context;

        public AsistenciaRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Estudiantes' with '_context.Set<Estudiante>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Estudiante DbSet.

        public async Task<IEnumerable<Asistencia>> GetAllAsync()
        {
            return await _context.Set<Asistencia>().ToListAsync();
        }

        public async Task<Asistencia> GetByIdAsync(int id)
        {
            return await _context.Set<Asistencia>().FindAsync(id);
        }

        public async Task AddAsync(Asistencia asistencia)
        {
            await _context.Set<Asistencia>().AddAsync(asistencia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asistencia asistencia)
        {
            _context.Set<Asistencia>().Update(asistencia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asistencia = await GetByIdAsync(id);
            if (asistencia != null)
            {
                _context.Set<Asistencia>().Remove(asistencia);
                await _context.SaveChangesAsync();
            }
        }
    }
}
