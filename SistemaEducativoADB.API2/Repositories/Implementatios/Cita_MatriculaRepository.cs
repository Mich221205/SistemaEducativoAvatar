using Microsoft.EntityFrameworkCore;
using SistemaEducativoADB.API2.Data;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class Cita_MatriculaRepository : ICita_MatriculaRepository
    {
        private readonly DbContext _context;

        public Cita_MatriculaRepository(DBContext context)
        {
            _context = context;
        }

        // Replace all occurrences of '_context.Cita_Matriculas' with '_context.Set<Cita_Matricula>()'
        // This uses the generic Set<TEntity>() method of DbContext to access the Cita_Matricula DbSet.

        public async Task<IEnumerable<Cita_Matricula>> GetAllAsync()
        {
            return await _context.Set<Cita_Matricula>().ToListAsync();
        }

        public async Task<Cita_Matricula> GetByIdAsync(int id)
        {
            return await _context.Set<Cita_Matricula>().FindAsync(id);
        }

        public async Task AddAsync(Cita_Matricula cita_matricula)
        {
            await _context.Set<Cita_Matricula>().AddAsync(cita_matricula);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cita_Matricula cita_matricula)
        {
            _context.Set<Cita_Matricula>().Update(cita_matricula);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cita_matricula = await GetByIdAsync(id);
            if (cita_matricula != null)
            {
                _context.Set<Cita_Matricula>().Remove(cita_matricula);
                await _context.SaveChangesAsync();
            }
        }
    }
}
