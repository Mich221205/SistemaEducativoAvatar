using SistemaEducativoADB.API2.Models.DTOs;
using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario Usuario);
        Task UpdateAsync(Usuario Usuario);
        Task DeleteAsync(int id);
        Task<Usuario?> LoginAsync(string email, string contrasena);
        Task CambiarEstadoAsync(int id, bool nuevoEstado);
        Task CambiarRolAsync(int id, int nuevoRol);
        Task ActualizarDatosProfesorAsync(int idUsuario, ActualizarProfesorDto dto);

    }
}
