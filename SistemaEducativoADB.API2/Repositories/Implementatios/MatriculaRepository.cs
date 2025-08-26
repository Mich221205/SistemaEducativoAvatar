using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly DBContext _context;

        public MatriculaRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            return await _context.Set<Matricula>().ToListAsync();
        }

        public async Task<Matricula?> GetByIdAsync(int id)
        {
            // FindAsync usa la PK; devuelve null si no existe
            return await _context.Set<Matricula>().FindAsync(id);
        }

        public async Task AddAsync(Matricula matricula)
        {
            // Valor por defecto si viene vacío
            if (string.IsNullOrWhiteSpace(matricula.estado))
                matricula.estado = "Pendiente";

            await _context.Set<Matricula>().AddAsync(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Matricula matricula)
        {
            _context.Set<Matricula>().Update(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Matricula>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Matricula>> GetByEstudianteAsync(int id_estudiante)
        {
            return await _context.Set<Matricula>()
                                 .Where(m => m.id_estudiante == id_estudiante)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Matricula>> GetByPeriodoAsync(int id_periodo)
        {
            return await _context.Set<Matricula>()
                                 .Where(m => m.id_periodo == id_periodo)
                                 .ToListAsync();
        }

        public async Task<bool> ExistsForEstudiantePeriodoAsync(int id_estudiante, int id_periodo)
        {
            return await _context.Set<Matricula>()
                                 .AnyAsync(m => m.id_estudiante == id_estudiante &&
                                                m.id_periodo == id_periodo);
        }
    }
}
