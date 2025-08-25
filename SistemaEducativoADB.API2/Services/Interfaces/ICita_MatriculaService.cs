using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface ICita_MatriculaService
    {
        Task<IEnumerable<Cita_Matricula>> GetAllCita_Matriculas();
        Task<Cita_Matricula> GetCita_MatriculaById(int id);
        Task AddCita_Matricula(Cita_Matricula cita_matricula);
        Task UpdateCita_Matricula(Cita_Matricula cita_matricula);
        Task DeleteCita_Matricula(int id);
    }
}
