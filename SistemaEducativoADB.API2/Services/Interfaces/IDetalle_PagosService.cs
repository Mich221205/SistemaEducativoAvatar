using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IDetalle_PagosService
    {
        Task<IEnumerable<DetallePago>> GetAllDetallePagos();
        Task<DetallePago?> GetDetallePagoById(int id);
        Task AddDetallePago(DetallePago detallePago);
        Task UpdateDetallePago(DetallePago detallePago);
        Task DeleteDetallePago(int id);
        Task<IEnumerable<DetallePago>> GetDetallePagosByPago(int idPago);
        Task<IEnumerable<DetallePago>> GetDetallePagosByMatricula(int idMatricula);
        Task<bool> ExistsAsync(int idPago, int idMatricula);
    }
}
