using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class EstudianteCreateDto
    {
        public int IdUsuario { get; set; }
        public string nombre { get; set; }
        public string carnet { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int? IdCarrera { get; set; }
        public Usuario Usuario { get; set; }
    }
}
