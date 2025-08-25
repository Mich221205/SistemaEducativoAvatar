using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class CorrequisitoService : ICorrequisitoService
    {
        private readonly ICorrequisitoRepository _repository;

        public CorrequisitoService(ICorrequisitoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Correquisito>> GetAllCorrequisitos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Correquisito> GetCorrequisitoById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddCorrequisito(Correquisito correquisito)
        {
            await _repository.AddAsync(correquisito);
        }

        public async Task UpdateCorrequisito(Correquisito correquisito)
        {
            await _repository.UpdateAsync(correquisito);
        }

        public async Task DeleteCorrequisito(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
