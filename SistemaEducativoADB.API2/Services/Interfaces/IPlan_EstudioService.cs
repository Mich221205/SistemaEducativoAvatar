using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IPlan_EstudioService
    {
        Task<IEnumerable<Plan_Estudio>> GetAllPlanes();
        Task<Plan_Estudio?> GetPlanById(int id);
        Task AddPlan(Plan_Estudio plan);
        Task UpdatePlan(Plan_Estudio plan);
        Task DeletePlan(int id);
        Task<IEnumerable<Plan_Estudio>> GetPlanesPorCarrera(int idCarrera);
        Task<Plan_Estudio?> GetPlanPorCarreraYAnio(int idCarrera, int anioInicio);
    }
}
