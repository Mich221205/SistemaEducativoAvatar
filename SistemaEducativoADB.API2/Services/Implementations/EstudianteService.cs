using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _repository;

        public EstudianteService(IEstudianteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Estudiante>> GetAllEstudiantes()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Estudiante> GetEstudianteById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddEstudiante(Estudiante estudiante)
        {
            await _repository.AddAsync(estudiante);
        }

        public async Task UpdateEstudiante(Estudiante estudiante)
        {
            await _repository.UpdateAsync(estudiante);
        }

        public async Task DeleteEstudiante(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
