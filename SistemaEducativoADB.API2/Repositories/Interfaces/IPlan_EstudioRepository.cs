using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IPlan_EstudioRepository
    {
        Task<IEnumerable<Plan_Estudio>> GetAllAsync();
        Task<Plan_Estudio?> GetByIdAsync(int id);
        Task AddAsync(Plan_Estudio plan);
        Task UpdateAsync(Plan_Estudio plan);
        Task DeleteAsync(int id);
        Task<IEnumerable<Plan_Estudio>> GetByCarreraAsync(int idCarrera);
        Task<Plan_Estudio?> GetByCarreraYAnioAsync(int idCarrera, int anioInicio);
    }
}
