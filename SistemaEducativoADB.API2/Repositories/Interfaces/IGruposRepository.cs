using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IGruposRepository
    {
        Task<IEnumerable<Grupo>> GetAllAsync();
        Task<Grupo?> GetByIdAsync(int id);
        Task AddAsync(Grupo grupo);
        Task UpdateAsync(Grupo grupo);
        Task DeleteAsync(int id);
        Task<IEnumerable<Grupo>> GetByMateriaAsync(int id_materia);
        Task<IEnumerable<Grupo>> GetByProfesorAsync(int id_profesor);
        Task<bool> ExistsForMateriaNumeroAsync(int id_materia, string grupo_numero);
    }
}
