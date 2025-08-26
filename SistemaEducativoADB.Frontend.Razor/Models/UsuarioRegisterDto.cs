namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class UsuarioRegisterDto
    {
        public string nombre { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        public int id_rol { get; set; } = 35; // por defecto Estudiante
    }
}

