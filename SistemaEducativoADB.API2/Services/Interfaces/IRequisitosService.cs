using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IRequisitosService
    {
        Task<IEnumerable<Requisito>> GetAllRequisitos();
        Task<Requisito?> GetRequisitoByIds(int idMateria, int idRequisito);
        Task<IEnumerable<Requisito>> GetRequisitosPorMateria(int idMateria);
        Task<IEnumerable<Requisito>> GetRequisitosPorRequisito(int idRequisito);
        Task AddRequisito(Requisito requisito);
        Task UpdateRequisito(Requisito requisito);
        Task DeleteRequisito(int idMateria, int idRequisito);
    }
}
