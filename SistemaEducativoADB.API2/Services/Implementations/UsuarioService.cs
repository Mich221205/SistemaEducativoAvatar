using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SistemaEducativoADB.API2.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddUsuario(Usuario Usuario)
        {
            await _repository.AddAsync(Usuario);
        }

        public async Task UpdateUsuario(Usuario Usuario)
        {
            await _repository.UpdateAsync(Usuario);
        }

        public async Task DeleteUsuario(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
