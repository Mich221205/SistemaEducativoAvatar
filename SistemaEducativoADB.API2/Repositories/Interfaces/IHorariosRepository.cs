using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IHorariosRepository
    {
        Task<IEnumerable<Horario>> GetAllAsync();
        Task<Horario?> GetByIdAsync(int id);
        Task AddAsync(Horario horario);
        Task UpdateAsync(Horario horario);
        Task DeleteAsync(int id);
        Task<IEnumerable<Horario>> GetByGrupoAsync(int id_grupo);
        Task<bool> ExistsOverlapAsync(int id_grupo, string dia_semana, TimeSpan hora_inicio, TimeSpan hora_fin);
    }
}
