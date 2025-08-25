using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _repository;

        public RolesService(IRolesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Rol>> GetAllRoles()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Rol?> GetRolById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddRol(Rol rol)
        {
            await _repository.AddAsync(rol);
        }

        public async Task UpdateRol(Rol rol)
        {
            await _repository.UpdateAsync(rol);
        }

        public async Task DeleteRol(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
