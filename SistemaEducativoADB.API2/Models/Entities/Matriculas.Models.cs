namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Matricula
    {
        public int id_matricula { get; set; }     // Clave primaria (IDENTITY)
        public int id_estudiante { get; set; }    // FK hacia Estudiante
        public int id_periodo { get; set; }       // FK hacia Periodo académico
        public string estado { get; set; } = "";  // Estado de la matrícula ("Confirmada", "Pendiente")

    }
}
