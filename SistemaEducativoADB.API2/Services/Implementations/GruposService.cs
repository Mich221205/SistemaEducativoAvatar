using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class GruposService : IGruposService
    {
        private readonly IGruposRepository _repository;

        public GruposService(IGruposRepository repository)
        {
            _repository = repository;
        }

        // ----------------- CRUD -----------------
        public async Task<IEnumerable<Grupo>> GetAllGrupos()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Grupo?> GetGrupoById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddGrupo(Grupo grupo)
        {
            grupo.GrupoNumero = (grupo.GrupoNumero ?? string.Empty).Trim();
            grupo.Aula = (grupo.Aula ?? string.Empty).Trim();

            await _repository.AddAsync(grupo);
        }

        public async Task UpdateGrupo(Grupo grupo)
        {
            grupo.GrupoNumero = (grupo.GrupoNumero ?? string.Empty).Trim();
            grupo.Aula = (grupo.Aula ?? string.Empty).Trim();

            await _repository.UpdateAsync(grupo);
        }

        public async Task DeleteGrupo(int id)
        {
            await _repository.DeleteAsync(id);
        }

        // ------------- Consultas -------------
        public async Task<IEnumerable<Grupo>> GetByMateria(int id_materia)
        {
            return await _repository.GetByMateriaAsync(id_materia);
        }

        public async Task<IEnumerable<Grupo>> GetByProfesor(int id_profesor)
        {
            return await _repository.GetByProfesorAsync(id_profesor);
        }

        public async Task<bool> ExistsForMateriaNumero(int id_materia, string grupo_numero)
        {
            return await _repository.ExistsForMateriaNumeroAsync(
                id_materia, (grupo_numero ?? string.Empty).Trim());
        }
    }
}
