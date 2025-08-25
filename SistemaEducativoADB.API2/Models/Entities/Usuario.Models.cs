using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Principal;

namespace SistemaEducativoADB.API2.Models.Entities
{

    public class Usuario
    {
        public int IdUsuario { get; set; } // id_usuario
        public string nombre { get; set; } // nombre
        public string email { get; set; } // email
        public string contrasena { get; set; } // contrasena

        public bool Estado { get; set; } // estado (1 = activo, 0 = inactivo)
        
        public DateTime FechaCreacion { get; set; } // fecha_creacion

        public Estudiante Estudiante { get; set; }
        public Profesor Profesor { get; set; }
    }

}
