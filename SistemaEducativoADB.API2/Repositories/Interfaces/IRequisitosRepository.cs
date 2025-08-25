using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IRequisitosRepository
    {
        Task<IEnumerable<Requisito>> GetAllAsync();
        Task<Requisito?> GetByIdsAsync(int idMateria, int idRequisito);
        Task<IEnumerable<Requisito>> GetByMateriaAsync(int idMateria);
        Task<IEnumerable<Requisito>> GetByRequisitoAsync(int idRequisito);
        Task AddAsync(Requisito requisito);
        Task UpdateAsync(Requisito requisito);
        Task DeleteAsync(int idMateria, int idRequisito);
    }
}
