using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class Plan_EstudioService : IPlan_EstudioService
    {
        private readonly IPlan_EstudioRepository _repository;

        public Plan_EstudioService(IPlan_EstudioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Plan_Estudio>> GetAllPlanes()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Plan_Estudio?> GetPlanById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddPlan(Plan_Estudio plan)
        {
            await _repository.AddAsync(plan);
        }

        public async Task UpdatePlan(Plan_Estudio plan)
        {
            await _repository.UpdateAsync(plan);
        }

        public async Task DeletePlan(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Plan_Estudio>> GetPlanesPorCarrera(int idCarrera)
        {
            return await _repository.GetByCarreraAsync(idCarrera);
        }

        public async Task<Plan_Estudio?> GetPlanPorCarreraYAnio(int idCarrera, int anioInicio)
        {
            return await _repository.GetByCarreraYAnioAsync(idCarrera, anioInicio);
        }
    }
}
