using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IAsistenciaRepository
    {
        Task<IEnumerable<Asistencia>> GetAllAsync();
        Task<Asistencia> GetByIdAsync(int id);
        Task AddAsync(Asistencia asistencia);
        Task UpdateAsync(Asistencia asistencia);
        Task DeleteAsync(int id);
    }
}
