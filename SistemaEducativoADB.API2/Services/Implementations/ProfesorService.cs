using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _repository;

        public ProfesorService(IProfesorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Profesor>> GetAllProfesors()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Profesor> GetProfesorById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddProfesor(Profesor Profesor)
        {
            await _repository.AddAsync(Profesor);
        }

        public async Task UpdateProfesor(Profesor Profesor)
        {
            await _repository.UpdateAsync(Profesor);
        }

        public async Task DeleteProfesor(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
