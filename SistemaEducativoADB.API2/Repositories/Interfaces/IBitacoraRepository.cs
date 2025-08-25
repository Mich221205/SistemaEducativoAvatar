using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IBitacoraRepository
    {
        Task<IEnumerable<Bitacora>> GetAllAsync();
        Task<Bitacora> GetByIdAsync(int id);
        Task AddAsync(Bitacora bitacora);
        Task UpdateAsync(Bitacora bitacora);
        Task DeleteAsync(int id);
    }
}
