using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativo.Frontend.Models
{
    public class ProfesorViewModel
    {
        public int IdProfesor { get; set; }
        public int IdUsuario { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string CorreoPersonal { get; set; }

        public Usuario Usuario { get; set; }
    }
}
