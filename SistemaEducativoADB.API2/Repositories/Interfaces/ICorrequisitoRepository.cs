using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface ICorrequisitoRepository
    {
        Task<IEnumerable<Correquisito>> GetAllAsync();
        Task<Correquisito> GetByIdAsync(int id);
        Task AddAsync(Correquisito estudiante);
        Task UpdateAsync(Correquisito estudiante);
        Task DeleteAsync(int id);
    }
}
