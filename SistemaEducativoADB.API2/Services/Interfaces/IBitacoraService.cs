using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IBitacoraService
    {
        Task<IEnumerable<Bitacora>> GetAllBitacoras();
        Task<Bitacora> GetBitacoraById(int id);
        Task AddBitacora(Bitacora bitacora);
        Task UpdateBitacora(Bitacora bitacora);
        Task DeleteBitacora(int id);
    }
}
