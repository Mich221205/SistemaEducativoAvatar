using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class Detalle_PagosService : IDetalle_PagosService
    {
        private readonly IDetalle_PagosRepository _repository;

        public Detalle_PagosService(IDetalle_PagosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DetallePago>> GetAllDetallePagos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DetallePago?> GetDetallePagoById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddDetallePago(DetallePago detallePago)
        {
            if (await _repository.ExistsAsync(detallePago.IdPago, detallePago.IdMatricula))
                throw new InvalidOperationException("Ya existe un detalle para ese pago y matrícula.");

            await _repository.AddAsync(detallePago);
        }

        public async Task UpdateDetallePago(DetallePago detallePago)
        {
            await _repository.UpdateAsync(detallePago);
        }

        public async Task DeleteDetallePago(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DetallePago>> GetDetallePagosByPago(int idPago)
        {
            return await _repository.GetByPagoAsync(idPago);
        }

        public async Task<IEnumerable<DetallePago>> GetDetallePagosByMatricula(int idMatricula)
        {
            return await _repository.GetByMatriculaAsync(idMatricula);
        }

        public async Task<bool> ExistsAsync(int idPago, int idMatricula)
        {
            return await _repository.ExistsAsync(idPago, idMatricula);
        }
    }
}
