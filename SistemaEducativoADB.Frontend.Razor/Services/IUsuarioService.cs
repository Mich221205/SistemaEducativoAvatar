using SistemaEducativoADB.Frontend.Razor.Models;

public interface IUsuarioService
{
    Task<Usuario?> LoginAsync(string email, string contrasena);
    Task<bool> RegisterAsync(UsuarioRegisterDto dto);
    Task<List<Usuario>?> GetAllUsuariosAsync();
    Task<List<Usuario>?> GetInactivosAsync();
    Task<bool> CambiarEstadoAsync(int idUsuario, bool nuevoEstado);
    Task<bool> CambiarRolAsync(int idUsuario, int nuevoRol);
}

