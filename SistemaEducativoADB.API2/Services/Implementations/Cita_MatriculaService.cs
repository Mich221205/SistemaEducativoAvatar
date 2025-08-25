using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class Cita_MatriculaService : ICita_MatriculaService
    {
        private readonly ICita_MatriculaRepository _repository;

        public Cita_MatriculaService(ICita_MatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Cita_Matricula>> GetAllCita_Matriculas()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Cita_Matricula> GetCita_MatriculaById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddCita_Matricula(Cita_Matricula cita_matricula)
        {
            await _repository.AddAsync(cita_matricula);
        }

        public async Task UpdateCita_Matricula(Cita_Matricula cita_matricula)
        {
            await _repository.UpdateAsync(cita_matricula);
        }

        public async Task DeleteCita_Matricula(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
