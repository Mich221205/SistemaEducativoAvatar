using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int id_rol { get; set; }
        public string RolNombre { get; set; } = string.Empty;
        
    }
}
