using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class Plan_EstudioRepository : IPlan_EstudioRepository
    {
        private readonly DBContext _context;

        public Plan_EstudioRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Plan_Estudio>> GetAllAsync()
        {
            return await _context.Set<Plan_Estudio>()
                .AsNoTracking()
                .Include(p => p.Carrera)
                .Include(p => p.Materias)
                .ToListAsync();
        }

        public async Task<Plan_Estudio?> GetByIdAsync(int id)
        {
            return await _context.Set<Plan_Estudio>()
                .AsNoTracking()
                .Include(p => p.Carrera)
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.id_plan == id);
        }

        public async Task AddAsync(Plan_Estudio plan)
        {
            await _context.Set<Plan_Estudio>().AddAsync(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Plan_Estudio plan)
        {
            _context.Set<Plan_Estudio>().Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Plan_Estudio>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Plan_Estudio>> GetByCarreraAsync(int idCarrera)
        {
            return await _context.Set<Plan_Estudio>()
                .AsNoTracking()
                .Include(p => p.Carrera)
                .Include(p => p.Materias)
                .Where(p => p.id_carrera == idCarrera)
                .ToListAsync();
        }

        public async Task<Plan_Estudio?> GetByCarreraYAnioAsync(int idCarrera, int anio)
        {
            return await _context.Set<Plan_Estudio>()
                .AsNoTracking()
                .Include(p => p.Carrera)
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.id_carrera == idCarrera && p.anio_inicio == anio);
        }
    }
}
