namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class NotaCompleta
    {
        public string Materia { get; set; } = "";
        public string Grupo { get; set; } = "";
        public decimal Nota { get; set; }
        public string Condicion { get; set; } = "";

        public Usuario Usuario { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
