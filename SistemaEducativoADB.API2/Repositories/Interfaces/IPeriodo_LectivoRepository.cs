using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IPeriodo_LectivoRepository
    {
        Task<IEnumerable<Periodo_Lectivo>> GetAllAsync();
        Task<Periodo_Lectivo> GetByIdAsync(int id);
        Task AddAsync(Periodo_Lectivo periodo);
        Task UpdateAsync(Periodo_Lectivo periodo);
        Task DeleteAsync(int id);
        Task<IEnumerable<Periodo_Lectivo>> GetByAnioAsync(int anio);
        Task<Periodo_Lectivo?> GetVigenteAsync(DateTime fecha);
    }
}
