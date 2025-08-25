using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class Detalle_MatriculaService : IDetalle_MatriculaService
    {
        private readonly IDetalle_MatriculaRepository _repository;

        public Detalle_MatriculaService(IDetalle_MatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Detalle_Matricula>> GetAllDetalle_Matriculas()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Detalle_Matricula> GetDetalle_MatriculaById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddDetalle_Matricula(Detalle_Matricula detalle_matricula)
        {
            await _repository.AddAsync(detalle_matricula);
        }

        public async Task UpdateDetalle_Matricula(Detalle_Matricula detalle_matricula)
        {
            await _repository.UpdateAsync(detalle_matricula);
        }

        public async Task DeleteDetalle_Matricula(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
