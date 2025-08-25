using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IDetalle_MatriculaRepository
    {
        Task<IEnumerable<Detalle_Matricula>> GetAllAsync();
        Task<Detalle_Matricula> GetByIdAsync(int id);
        Task AddAsync(Detalle_Matricula detalle_matricula);
        Task UpdateAsync(Detalle_Matricula detalle_matricula);
        Task DeleteAsync(int id);
    }
}
