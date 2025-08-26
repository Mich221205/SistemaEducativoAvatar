using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _repository;

        public MatriculaService(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Matricula>> GetAllMatriculas()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Matricula?> GetMatriculaById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Matricula>> GetByEstudiante(int id_estudiante)
        {
            return await _repository.GetByEstudianteAsync(id_estudiante);
        }

        public async Task<IEnumerable<Matricula>> GetByPeriodo(int id_periodo)
        {
            return await _repository.GetByPeriodoAsync(id_periodo);
        }

        public async Task<bool> ExistsForEstudiantePeriodo(int id_estudiante, int id_periodo)
        {
            return await _repository.ExistsForEstudiantePeriodoAsync(id_estudiante, id_periodo);
        }

        public async Task AddMatricula(Matricula matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula.estado))
                matricula.estado = "Pendiente";

            await _repository.AddAsync(matricula);
        }

        public async Task UpdateMatricula(Matricula matricula)
        {
            await _repository.UpdateAsync(matricula);
        }

        public async Task DeleteMatricula(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
