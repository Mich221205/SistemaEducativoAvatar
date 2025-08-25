
using SistemaEducativoADB.API2.Models.Entities;
namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
        public interface IDetalle_PagosRepository
    {
        Task<IEnumerable<DetallePago>> GetAllAsync();
        Task<DetallePago?> GetByIdAsync(int id);
        Task AddAsync(DetallePago detallePago);
        Task UpdateAsync(DetallePago detallePago);
        Task DeleteAsync(int id);
        Task<IEnumerable<DetallePago>> GetByPagoAsync(int id_pago);
        Task<IEnumerable<DetallePago>> GetByMatriculaAsync(int id_matricula);
        Task<bool> ExistsAsync(int id_pago, int id_matricula);
    }
}
