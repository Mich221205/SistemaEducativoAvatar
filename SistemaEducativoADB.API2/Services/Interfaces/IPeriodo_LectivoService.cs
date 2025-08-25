using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IPeriodo_LectivoService
    {
        Task<IEnumerable<Periodo_Lectivo>> GetAllPeriodos();
        Task<Periodo_Lectivo?> GetPeriodoById(int id);
        Task AddPeriodo(Periodo_Lectivo periodo);
        Task UpdatePeriodo(Periodo_Lectivo periodo);
        Task DeletePeriodo(int id);
        Task<IEnumerable<Periodo_Lectivo>> GetPeriodosPorAnio(int anio);
        Task<Periodo_Lectivo?> GetPeriodoVigente(DateTime fecha);
    }
}

