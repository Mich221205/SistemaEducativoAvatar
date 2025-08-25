using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _repository;

        public BitacoraService(IBitacoraRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Bitacora>> GetAllBitacoras()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Bitacora> GetBitacoraById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddBitacora(Bitacora bitacora)
        {
            await _repository.AddAsync(bitacora);
        }

        public async Task UpdateBitacora(Bitacora bitacora)
        {
            await _repository.UpdateAsync(bitacora);
        }

        public async Task DeleteBitacora(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
