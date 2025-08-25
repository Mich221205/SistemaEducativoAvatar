using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class HorariosService : IHorariosService
    {
        private readonly IHorariosRepository _repository;

        public HorariosService(IHorariosRepository repository)
        {
            _repository = repository;
        }

        // ----------------- CRUD -----------------
        public async Task<IEnumerable<Horario>> GetAllHorarios()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Horario?> GetHorarioById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddHorario(Horario horario)
        {
            horario.DiaSemana = (horario.DiaSemana ?? string.Empty).Trim();

            await _repository.AddAsync(horario);
        }

        public async Task UpdateHorario(Horario horario)
        {
            horario.DiaSemana = (horario.DiaSemana ?? string.Empty).Trim();

            await _repository.UpdateAsync(horario);
        }

        public async Task DeleteHorario(int id)
        {
            await _repository.DeleteAsync(id);
        }

        // ------------- Consultas -------------
        public async Task<IEnumerable<Horario>> GetByGrupo(int id_grupo)
        {
            return await _repository.GetByGrupoAsync(id_grupo);
        }

        public async Task<bool> ExistsOverlap(int id_grupo, string dia_semana, TimeSpan hora_inicio, TimeSpan hora_fin)
        {
            return await _repository.ExistsOverlapAsync(
                id_grupo, (dia_semana ?? string.Empty).Trim(), hora_inicio, hora_fin);
        }
    }
}
