using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class Detalle_MatriculaRepository : IDetalle_MatriculaRepository
    {
        private readonly DbContext _context;

        public Detalle_MatriculaRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Detalle_Matriculas' with '_context.Set<Detalle_Matricula>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Detalle_Matricula DbSet.

        public async Task<IEnumerable<Detalle_Matricula>> GetAllAsync()
        {
            return await _context.Set<Detalle_Matricula>().ToListAsync();
        }

        public async Task<Detalle_Matricula> GetByIdAsync(int id)
        {
            return await _context.Set<Detalle_Matricula>().FindAsync(id);
        }

        public async Task AddAsync(Detalle_Matricula detalle_matricula)
        {
            await _context.Set<Detalle_Matricula>().AddAsync(detalle_matricula);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Detalle_Matricula detalle_matricula)
        {
            _context.Set<Detalle_Matricula>().Update(detalle_matricula);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var detalle_matricula = await GetByIdAsync(id);
            if (detalle_matricula != null)
            {
                _context.Set<Detalle_Matricula>().Remove(detalle_matricula);
                await _context.SaveChangesAsync();
            }
        }
    }
}
