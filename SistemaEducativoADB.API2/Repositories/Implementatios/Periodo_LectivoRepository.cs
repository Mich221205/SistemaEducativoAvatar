using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class Periodo_LectivoRepository : IPeriodo_LectivoRepository
    {
        private readonly DBContext _context;

        public Periodo_LectivoRepository(DBContext context)
        {
            _context = context;
        }

        // ---------- CRUD básico ----------
        public async Task<IEnumerable<Periodo_Lectivo>> GetAllAsync()
        {
            return await _context.Set<Periodo_Lectivo>()
                                 .OrderBy(p => p.anio)
                                 .ThenBy(p => p.cuatrimestre)
                                 .ToListAsync();
        }

        public async Task<Periodo_Lectivo> GetByIdAsync(int id)
        {
            return await _context.Set<Periodo_Lectivo>().FindAsync(id);
        }

        public async Task AddAsync(Periodo_Lectivo periodo)
        {
            await _context.Set<Periodo_Lectivo>().AddAsync(periodo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Periodo_Lectivo periodo)
        {
            _context.Set<Periodo_Lectivo>().Update(periodo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Periodo_Lectivo>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ---------- Consultas útiles (opcionales) ----------
        public async Task<IEnumerable<Periodo_Lectivo>> GetByAnioAsync(int anio)
        {
            return await _context.Set<Periodo_Lectivo>()
                                 .Where(p => p.anio == anio)
                                 .OrderBy(p => p.cuatrimestre)
                                 .ToListAsync();
        }

        public async Task<Periodo_Lectivo?> GetVigenteAsync(DateTime fecha)
        {
            return await _context.Set<Periodo_Lectivo>()
                                 .FirstOrDefaultAsync(p => p.fecha_inicio <= fecha && p.fecha_fin >= fecha);
        }
    }
}
