using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class Periodo_LectivoService : IPeriodo_LectivoService
    {
        private readonly IPeriodo_LectivoRepository _repository;

        public Periodo_LectivoService(IPeriodo_LectivoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Periodo_Lectivo>> GetAllPeriodos()
            => await _repository.GetAllAsync();

        public async Task<Periodo_Lectivo?> GetPeriodoById(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddPeriodo(Periodo_Lectivo periodo)
            => await _repository.AddAsync(periodo);

        public async Task UpdatePeriodo(Periodo_Lectivo periodo)
            => await _repository.UpdateAsync(periodo);

        public async Task DeletePeriodo(int id)
            => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<Periodo_Lectivo>> GetPeriodosPorAnio(int anio)
            => await _repository.GetByAnioAsync(anio);

        public async Task<Periodo_Lectivo?> GetPeriodoVigente(DateTime fecha)
            => await _repository.GetVigenteAsync(fecha);
    }
}
