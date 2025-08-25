using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IHorariosService
    {
        Task<IEnumerable<Horario>> GetAllHorarios();
        Task<Horario?> GetHorarioById(int id);
        Task AddHorario(Horario horario);
        Task UpdateHorario(Horario horario);
        Task DeleteHorario(int id);
        Task<IEnumerable<Horario>> GetByGrupo(int id_grupo);
        Task<bool> ExistsOverlap(int id_grupo, string dia_semana, TimeSpan hora_inicio, TimeSpan hora_fin);
    }
}
