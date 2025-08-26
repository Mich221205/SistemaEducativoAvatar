using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class PagosRepository : IPagosRepository
    {
        private readonly DBContext _context;

        public PagosRepository(DBContext context)
        {
            _context = context;
        }

        // ------------------ CRUD ------------------
        public async Task<IEnumerable<Pago>> GetAllAsync()
        {
            return await _context.Set<Pago>()
                .AsNoTracking()
                .Include(p => p.Estudiante)
                .ToListAsync();
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _context.Set<Pago>()
                .AsNoTracking()
                .Include(p => p.Estudiante)
                .FirstOrDefaultAsync(p => p.IdPago == id);
        }

        public async Task AddAsync(Pago pago)
        {
            pago.Estado = (pago.Estado ?? string.Empty).Trim();
            pago.MetodoPago = (pago.MetodoPago ?? string.Empty).Trim();

            await _context.Set<Pago>().AddAsync(pago);
            await _context.SaveChangesAsync();

            await _context.Entry(pago).Reference(p => p.Estudiante).LoadAsync();
            if (pago.Estudiante != null)
            {
                await _context.Entry(pago.Estudiante).Reference(e => e.Usuario).LoadAsync();
            }
        }

        public async Task UpdateAsync(Pago pago)
        {
            pago.Estado = (pago.Estado ?? string.Empty).Trim();
            pago.MetodoPago = (pago.MetodoPago ?? string.Empty).Trim();

            _context.Set<Pago>().Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Pago>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Pago>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // --------------- Consultas ---------------
        public async Task<IEnumerable<Pago>> GetByEstudianteAsync(int id_estudiante)
        {
            return await _context.Set<Pago>()
                .AsNoTracking()
                .Include(p => p.Estudiante)
                .Where(p => p.IdEstudiante == id_estudiante)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pago>> GetByEstadoAsync(string estado)
        {
            var e = (estado ?? string.Empty).Trim();
            return await _context.Set<Pago>()
                .AsNoTracking()
                .Include(p => p.Estudiante)
                .Where(p => p.Estado == e)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pago>> GetByRangoFechasAsync(DateTime desde, DateTime hasta)
        {
            // incluye ambos extremos
            return await _context.Set<Pago>()
                .AsNoTracking()
                .Include(p => p.Estudiante)
                .Where(p => p.Fecha >= desde && p.Fecha <= hasta)
                .ToListAsync();
        }

        public async Task<bool> UpdateEstadoAsync(int id_pago, string nuevoEstado)
        {
            var entity = await _context.Set<Pago>().FindAsync(id_pago);
            if (entity == null) return false;

            entity.Estado = (nuevoEstado ?? string.Empty).Trim();
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
