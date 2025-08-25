using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<Matricula>> GetAllAsync();
        Task<Matricula?> GetByIdAsync(int id);
        Task AddAsync(Matricula matricula);
        Task UpdateAsync(Matricula matricula);
        Task DeleteAsync(int id);
        Task<IEnumerable<Matricula>> GetByEstudianteAsync(int id_estudiante);
        Task<IEnumerable<Matricula>> GetByPeriodoAsync(int id_periodo);
        Task<bool> ExistsForEstudiantePeriodoAsync(int id_estudiante, int id_periodo);
    }
}
