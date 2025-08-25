using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface ICorrequisitoService
    {
        Task<IEnumerable<Correquisito>> GetAllCorrequisitos();
        Task<Correquisito> GetCorrequisitoById(int id);
        Task AddCorrequisito(Correquisito correquisito);
        Task UpdateCorrequisito(Correquisito correquisito);
        Task DeleteCorrequisito(int id);
    }
}
