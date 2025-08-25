using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IPagosRepository
    {
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task AddAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
        Task<IEnumerable<Pago>> GetByEstudianteAsync(int id_estudiante);
        Task<IEnumerable<Pago>> GetByEstadoAsync(string estado);
        Task<IEnumerable<Pago>> GetByRangoFechasAsync(DateTime desde, DateTime hasta);
        Task<bool> UpdateEstadoAsync(int id_pago, string nuevoEstado);
    }
}
