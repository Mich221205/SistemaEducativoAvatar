using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IPagosService
    {
        Task<IEnumerable<Pago>> GetAllPagos();
        Task<Pago?> GetPagoById(int id);
        Task AddPago(Pago pago);
        Task UpdatePago(Pago pago);
        Task DeletePago(int id);
        Task<IEnumerable<Pago>> GetPagosPorEstudiante(int id_estudiante);
        Task<IEnumerable<Pago>> GetPagosPorEstado(string estado);
        Task<IEnumerable<Pago>> GetPagosPorRangoFechas(DateTime desde, DateTime hasta);
        Task<bool> CambiarEstadoPago(int id_pago, string nuevoEstado);
    }
}
