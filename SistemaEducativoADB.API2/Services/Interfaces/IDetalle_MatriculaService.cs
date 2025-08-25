using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IDetalle_MatriculaService
    {
        Task<IEnumerable<Detalle_Matricula>> GetAllDetalle_Matriculas();
        Task<Detalle_Matricula> GetDetalle_MatriculaById(int id);
        Task AddDetalle_Matricula(Detalle_Matricula detalle_matricula);
        Task UpdateDetalle_Matricula(Detalle_Matricula detalle_matricula);
        Task DeleteDetalle_Matricula(int id);
    }
}
