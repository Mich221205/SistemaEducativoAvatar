using SistemaEducativoADB.Frontend.Razor.Models;
using SistemaEducativo.Frontend.Models;

namespace SistemaEducativoADB.Frontend.Razor.Services
{ 
    public interface IUsuarioService
    {
        Task<Usuario?> LoginAsync(string email, string contrasena);
        Task<bool> RegisterAsync(UsuarioRegisterDto dto);
        Task<List<Usuario>?> GetAllUsuariosAsync();
        Task<List<Usuario>?> GetInactivosAsync();
        Task<bool> CambiarEstadoAsync(int idUsuario, bool nuevoEstado);
        Task<bool> CambiarRolAsync(int idUsuario, int nuevoRol);
        Task<List<EstudianteDto>> GetEstudiantesActivosAsync();
        Task<bool> ActualizarDatosProfesorAsync(int idUsuario, ActualizarProfesorDto dto);
        Task<ActualizarProfesorDto?> GetDatosProfesorAsync(int idUsuario);
    }
}

