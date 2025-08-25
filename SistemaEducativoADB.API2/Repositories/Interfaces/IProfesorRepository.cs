using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IProfesorRepository
    {
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor> GetByIdAsync(int id);
        Task AddAsync(Profesor Profesor);
        Task UpdateAsync(Profesor Profesor);
        Task DeleteAsync(int id);
    }
}
