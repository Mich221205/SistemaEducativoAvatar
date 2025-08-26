using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class CarreraService : ICarreraService
    {
        private readonly ICarreraRepository _repository;

        public CarreraService(ICarreraRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Carrera>> GetAllCarreras()
        {
            var carreras = await _repository.GetAllAsync();
            return carreras.ToList();
        }

        public async Task<Carrera?> GetCarreraById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddCarrera(Carrera carrera)
        {
            await _repository.AddAsync(carrera);
        }

        public async Task UpdateCarrera(Carrera carrera)
        {
            await _repository.UpdateAsync(carrera);
        }

        public async Task DeleteCarrera(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
