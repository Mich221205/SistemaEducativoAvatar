using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IRolesService
    {
        Task<IEnumerable<Rol>> GetAllRoles();
        Task<Rol?> GetRolById(int id);
        Task AddRol(Rol rol);
        Task UpdateRol(Rol rol);
        Task DeleteRol(int id);
    }
}
