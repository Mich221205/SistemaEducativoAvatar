using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativoADB.API2.Repositories.Implementatios
{
    public class HorariosRepository : IHorariosRepository
    {
        private readonly DBContext _context;
        public HorariosRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Horario>> GetAllAsync()
        {
            return await _context.Set<Horario>()
                .AsNoTracking()
                .Include(h => h.Grupo)
                .ToListAsync();
        }

        public async Task<Horario?> GetByIdAsync(int id)
        {
            return await _context.Set<Horario>()
                .AsNoTracking()
                .Include(h => h.Grupo)
                .FirstOrDefaultAsync(h => h.IdHorario == id);
        }

        public async Task<IEnumerable<Horario>> GetByGrupoAsync(int id_grupo)
        {
            return await _context.Set<Horario>()
                .AsNoTracking()
                .Include(h => h.Grupo)
                .Where(h => h.IdGrupo == id_grupo)
                .ToListAsync();
        }

        public async Task AddAsync(Horario horario)
        {
            await _context.Set<Horario>().AddAsync(horario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Horario horario)
        {
            _context.Set<Horario>().Update(horario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Horario>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Horario>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsOverlapAsync(int id_grupo, string dia_semana, TimeSpan hora_inicio, TimeSpan hora_fin)
        {
            var dia = (dia_semana ?? string.Empty).Trim();

            return await _context.Set<Horario>()
                .AnyAsync(h =>
                    h.IdGrupo == id_grupo &&
                    h.DiaSemana == dia &&
                    hora_inicio < h.HoraFin &&
                    hora_fin > h.HoraInicio);
        }
    }
}
