using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class GruposRepository : IGruposRepository
    {
        private readonly DBContext _context;

        public GruposRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grupo>> GetAllAsync()
        {
            return await _context.Set<Grupo>()
                .AsNoTracking()
                .Include(g => g.Materia)
                .Include(g => g.Profesor)
                    .ThenInclude(p => p.Usuario)
                .ToListAsync();
        }

        public async Task<Grupo?> GetByIdAsync(int id)
        {
            return await _context.Set<Grupo>()
                .AsNoTracking()
                .Include(g => g.Materia)
                .Include(g => g.Profesor)
                    .ThenInclude(p => p.Usuario)
                .FirstOrDefaultAsync(g => g.IdGrupo == id);
        }

        public async Task<IEnumerable<Grupo>> GetByMateriaAsync(int id_materia)
        {
            return await _context.Set<Grupo>()
                .AsNoTracking()
                .Include(g => g.Materia)
                .Include(g => g.Profesor)
                    .ThenInclude(p => p.Usuario)
                .Where(g => g.IdMateria == id_materia)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grupo>> GetByProfesorAsync(int id_profesor)
        {
            return await _context.Set<Grupo>()
                .AsNoTracking()
                .Include(g => g.Materia)
                .Include(g => g.Profesor)
                    .ThenInclude(p => p.Usuario)
                .Where(g => g.IdProfesor == id_profesor)
                .ToListAsync();
        }

        public async Task AddAsync(Grupo grupo)
        {
            await _context.Set<Grupo>().AddAsync(grupo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Grupo grupo)
        {
            _context.Set<Grupo>().Update(grupo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Grupo>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Grupo>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsForMateriaNumeroAsync(int id_materia, string grupo_numero)
        {
            var num = (grupo_numero ?? string.Empty).Trim();
            return await _context.Set<Grupo>()
                .AsNoTracking()
                .AnyAsync(g => g.IdMateria == id_materia && g.GrupoNumero == num);
        }
    }
}
