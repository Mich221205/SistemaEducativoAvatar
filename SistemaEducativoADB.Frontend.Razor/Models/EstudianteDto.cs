namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class EstudianteDto
    {
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }

        public Usuario Usuario { get; set; }
    }
}

