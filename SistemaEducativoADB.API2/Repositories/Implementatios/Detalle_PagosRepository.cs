using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class Detalle_PagosRepository : IDetalle_PagosRepository
    {
        private readonly DBContext _context;

        public Detalle_PagosRepository(DBContext context)
        {
            _context = context;
        }
        // --------- CRUD ---------
        public async Task<IEnumerable<DetallePago>> GetAllAsync()
        {
            return await _context.Set<DetallePago>()
                         .AsNoTracking()
                         .Include(d => d.Pago)
                         .Include(d => d.Matricula)
                         .ToListAsync();
        }

        public async Task<DetallePago?> GetByIdAsync(int id)
        {
            return await _context.Set<DetallePago>()
                         .AsNoTracking()
                         .Include(d => d.Pago)
                         .Include(d => d.Matricula)
                         .FirstOrDefaultAsync(d => d.IdDetallePago == id);
        }

        public async Task AddAsync(DetallePago detallePago)
        {
            await _context.Set<DetallePago>().AddAsync(detallePago);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DetallePago detallePago)
        {
            _context.Set<DetallePago>().Update(detallePago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<DetallePago>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<DetallePago>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DetallePago>> GetByPagoAsync(int id_pago)
        {
            return await _context.Set<DetallePago>()
                        .AsNoTracking()
                        .Include(d => d.Pago)
                        .Include(d => d.Matricula)
                        .Where(d => d.IdPago == id_pago)
                        .ToListAsync();
        }

        public async Task<IEnumerable<DetallePago>> GetByMatriculaAsync(int id_matricula)
        {
            return await _context.Set<DetallePago>()
                          .AsNoTracking()
                          .Include(d => d.Pago)
                          .Include(d => d.Matricula)
                          .Where(d => d.IdMatricula == id_matricula)
                          .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id_pago, int id_matricula)
        {
            return await _context.Set<DetallePago>()
                                 .AsNoTracking()
                                 .AnyAsync(d => d.IdPago == id_pago &&
                                                d.IdMatricula == id_matricula);
        }
    }
}
