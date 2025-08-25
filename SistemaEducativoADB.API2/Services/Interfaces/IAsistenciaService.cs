using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IAsistenciaService
    {
        Task<IEnumerable<Asistencia>> GetAllAsistencias();
        Task<Asistencia> GetAsistenciaById(int id);
        Task AddAsistencia(Asistencia asistencia);
        Task UpdateAsistencia(Asistencia asistencia);
        Task DeleteAsistencia(int id);
    }
}
