using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface ICarreraRepository
    {
        Task<IEnumerable<Carrera>> GetAllAsync();
        Task<Carrera> GetByIdAsync(int id);
        Task AddAsync(Carrera Carrera);
        Task UpdateAsync(Carrera Carrera);
        Task DeleteAsync(int id);
    }
}
