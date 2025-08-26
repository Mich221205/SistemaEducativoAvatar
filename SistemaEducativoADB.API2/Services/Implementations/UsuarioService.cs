using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;
using SistemaEducativoADB.API2.Repositories.Interfaces;
using SistemaEducativoADB.API2.Services.Interfaces;

namespace SistemaEducativoADB.API2.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task ActualizarDatosProfesorAsync(int idUsuario, ActualizarProfesorDto dto)
        {
            await _repository.ActualizarDatosProfesorAsync(idUsuario, dto);
        }


        public async Task<List<Usuario>> GetAllUsuarios()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.ToList();
        }

        public async Task<Usuario?> GetUsuarioById(int id)
        {
            return await _repository.GetByIdAsync(id); // puede ser null
        }

        public async Task AddUsuario(Usuario usuario)
        {
            await _repository.AddAsync(usuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            await _repository.UpdateAsync(usuario);
        }

        public async Task DeleteUsuario(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Usuario?> Login(string email, string contrasena)
        {
            return await _repository.LoginAsync(email, contrasena);
        }

        public async Task CambiarEstadoAsync(int id, bool nuevoEstado)
        {
            await _repository.CambiarEstadoAsync(id, nuevoEstado);
        }

        public async Task CambiarRolAsync(int id, int nuevoRol)
        {
            await _repository.CambiarRolAsync(id, nuevoRol);
        }
    }
}
