using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class CarreraService(ICarreraRepository repository) : ICarreraService
    {
        private readonly ICarreraRepository _repository = repository;

        public async Task<IEnumerable<Carrera>> GetAllCarreras()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Carrera> GetCarreraById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddCarrera(Carrera Carrera)
        {
            await _repository.AddAsync(Carrera);
        }

        public async Task UpdateCarrera(Carrera Carrera)
        {
            await _repository.UpdateAsync(Carrera);
        }

        public async Task DeleteCarrera(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
