using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface ICita_MatriculaRepository
    {
        Task<IEnumerable<Cita_Matricula>> GetAllAsync();
        Task<Cita_Matricula> GetByIdAsync(int id);
        Task AddAsync(Cita_Matricula cita_matricula);
        Task UpdateAsync(Cita_Matricula cita_matricula);
        Task DeleteAsync(int id);
    }
}
