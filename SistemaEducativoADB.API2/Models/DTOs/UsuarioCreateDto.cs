namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class UsuarioCreateDto
    {
        public int IdUsuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int id_rol { get; set; }
        public string RolNombre { get; set; } = string.Empty;
    }
}


