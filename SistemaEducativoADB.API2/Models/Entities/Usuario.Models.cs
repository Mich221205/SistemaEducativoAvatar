using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int IdRol { get; set; }
        public Rol Rol { get; set; }

        [NotMapped] 
        public string RolNombre { get; set; } = string.Empty;

        public Estudiante? Estudiante { get; set; }
        public Profesor? Profesor { get; set; }
    }
}

