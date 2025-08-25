using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IMatriculaService
    {
        Task<IEnumerable<Matricula>> GetAllMatriculas();
        Task<Matricula?> GetMatriculaById(int id);
        Task<IEnumerable<Matricula>> GetByEstudiante(int id_estudiante);
        Task<IEnumerable<Matricula>> GetByPeriodo(int id_periodo);
        Task<bool> ExistsForEstudiantePeriodo(int id_estudiante, int id_periodo);
        Task AddMatricula(Matricula matricula);
        Task UpdateMatricula(Matricula matricula);
        Task DeleteMatricula(int id);
    }
}
