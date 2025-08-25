using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IMateriaService
    {
        Task<IEnumerable<Materia>> GetAllMaterias();
        Task<Materia> GetMateriaById(int id);
        Task AddMateria(Materia materia);
        Task UpdateMateria(Materia materia);
        Task DeleteMateria(int id);
    }
}