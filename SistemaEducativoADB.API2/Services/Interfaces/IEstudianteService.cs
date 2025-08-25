using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<Estudiante>> GetAllEstudiantes();
        Task<Estudiante> GetEstudianteById(int id);
        Task AddEstudiante(Estudiante estudiante);
        Task UpdateEstudiante(Estudiante estudiante);
        Task DeleteEstudiante(int id);
    }
}
