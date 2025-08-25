using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly IAsistenciaRepository _repository;

        public AsistenciaService(IAsistenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Asistencia>> GetAllAsistencia()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Asistencia> GetAsistenciaById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsistencia(Asistencia asistencia)
        {
            await _repository.AddAsync(asistencia);
        }

        public async Task UpdateAsistencia(Asistencia asistencia)
        {
            await _repository.UpdateAsync(asistencia);
        }

        public async Task DeleteAsistencia(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
