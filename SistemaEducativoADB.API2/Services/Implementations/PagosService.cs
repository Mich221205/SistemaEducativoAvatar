using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class PagosService : IPagosService
    {
        private readonly IPagosRepository _repository;

        public PagosService(IPagosRepository repository)
        {
            _repository = repository;
        }

        // ----------------- CRUD -----------------
        public async Task<IEnumerable<Pago>> GetAllPagos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pago?> GetPagoById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddPago(Pago pago)
        {
            pago.Estado = string.IsNullOrWhiteSpace(pago.Estado) ? "Pendiente" : pago.Estado.Trim();
            pago.MetodoPago = (pago.MetodoPago ?? string.Empty).Trim();

            if (pago.Fecha == default)
                pago.Fecha = DateTime.UtcNow;

            await _repository.AddAsync(pago);
        }

        public async Task UpdatePago(Pago pago)
        {
            pago.Estado = (pago.Estado ?? string.Empty).Trim();
            pago.MetodoPago = (pago.MetodoPago ?? string.Empty).Trim();

            await _repository.UpdateAsync(pago);
        }

        public async Task DeletePago(int id)
        {
            await _repository.DeleteAsync(id);
        }

        // ------------- Consultas -------------
        public async Task<IEnumerable<Pago>> GetPagosPorEstudiante(int id_estudiante)
        {
            return await _repository.GetByEstudianteAsync(id_estudiante);
        }

        public async Task<IEnumerable<Pago>> GetPagosPorEstado(string estado)
        {
            return await _repository.GetByEstadoAsync((estado ?? string.Empty).Trim());
        }

        public async Task<IEnumerable<Pago>> GetPagosPorRangoFechas(DateTime desde, DateTime hasta)
        {
            return await _repository.GetByRangoFechasAsync(desde, hasta);
        }

        public async Task<bool> CambiarEstadoPago(int id_pago, string nuevoEstado)
        {
            return await _repository.UpdateEstadoAsync(id_pago, (nuevoEstado ?? string.Empty).Trim());
        }
    }
}
