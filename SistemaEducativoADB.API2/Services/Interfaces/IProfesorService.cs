using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IProfesorService
    {
        Task<IEnumerable<Profesor>> GetAllProfesors();
        Task<Profesor> GetProfesorById(int id);
        Task AddProfesor(Profesor Profesor);
        Task UpdateProfesor(Profesor Profesor);
        Task DeleteProfesor(int id);
    }
}
