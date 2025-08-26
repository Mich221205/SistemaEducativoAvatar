using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllUsuarios();           // Lista completa
        Task<Usuario?> GetUsuarioById(int id);          // Puede ser null si no existe
        Task AddUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
        Task CambiarEstadoAsync(int id, bool nuevoEstado);
        Task CambiarRolAsync(int id, int nuevoRol);
        Task<Usuario?> Login(string email, string contrasena); // Para login
    }
}

