using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class RequisitosRepository : IRequisitosRepository
    {
        private readonly DBContext _context;

        public RequisitosRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Requisito>> GetAllAsync()
        {
            return await _context.Set<Requisito>()
                .AsNoTracking()
                .Include(r => r.Materia)
                .Include(r => r.MateriaRequisito)
                .ToListAsync();
        }

        public async Task<Requisito?> GetByIdsAsync(int idMateria, int idRequisito)
        {
            return await _context.Set<Requisito>()
                .AsNoTracking()
                .Include(r => r.Materia)
                .Include(r => r.MateriaRequisito)
                .FirstOrDefaultAsync(r => r.IdMateria == idMateria && r.IdRequisito == idRequisito);
        }

        public async Task<IEnumerable<Requisito>> GetByMateriaAsync(int idMateria)
        {
            return await _context.Set<Requisito>()
                .AsNoTracking()
                .Include(r => r.Materia)
                .Include(r => r.MateriaRequisito)
                .Where(r => r.IdMateria == idMateria)
                .ToListAsync();
        }

        public async Task<IEnumerable<Requisito>> GetByRequisitoAsync(int idRequisito)
        {
            return await _context.Set<Requisito>()
                .AsNoTracking()
                .Include(r => r.Materia)
                .Include(r => r.MateriaRequisito)
                .Where(r => r.IdRequisito == idRequisito)
                .ToListAsync();
        }

        public async Task AddAsync(Requisito requisito)
        {
            await _context.Set<Requisito>().AddAsync(requisito);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Requisito requisito)
        {
            _context.Set<Requisito>().Update(requisito);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idMateria, int idRequisito)
        {
            // Para PK compuesta, FindAsync acepta los valores en el orden de la clave
            var entity = await _context.Set<Requisito>().FindAsync(idMateria, idRequisito);
            if (entity != null)
            {
                _context.Set<Requisito>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
