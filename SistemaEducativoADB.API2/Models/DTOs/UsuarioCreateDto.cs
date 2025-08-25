namespace SistemaEducativoADB.API.Models.DTOs
{
    public class UsuarioCreateDto
    {
        public int IdUsuario { get; set; } // id_usuario
        public string nombre { get; set; } // nombre
        public string email { get; set; } // email
        public string contrasena { get; set; } // contrasena

        public bool Estado { get; set; } // estado (1 = activo, 0 = inactivo)

        public DateTime FechaCreacion { get; set; } // fecha_creacion
    }
}
