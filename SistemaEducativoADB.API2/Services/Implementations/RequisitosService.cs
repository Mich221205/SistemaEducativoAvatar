using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class RequisitosService : IRequisitosService
    {
        private readonly IRequisitosRepository _repository;

        public RequisitosService(IRequisitosRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Requisito>> GetAllRequisitos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Requisito?> GetRequisitoByIds(int idMateria, int idRequisito)
        {
            return await _repository.GetByIdsAsync(idMateria, idRequisito);
        }

        public async Task<IEnumerable<Requisito>> GetRequisitosPorMateria(int idMateria)
        {
            return await _repository.GetByMateriaAsync(idMateria);
        }

        public async Task<IEnumerable<Requisito>> GetRequisitosPorRequisito(int idRequisito)
        {
            return await _repository.GetByRequisitoAsync(idRequisito);
        }

        public async Task AddRequisito(Requisito requisito)
        {
            await _repository.AddAsync(requisito);
        }

        public async Task UpdateRequisito(Requisito requisito)
        {
            await _repository.UpdateAsync(requisito);
        }

        public async Task DeleteRequisito(int idMateria, int idRequisito)
        {
            await _repository.DeleteAsync(idMateria, idRequisito);
        }
    }
}
