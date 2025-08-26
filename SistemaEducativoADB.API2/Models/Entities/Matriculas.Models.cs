namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Matricula
    {
        public int id_matricula { get; set; }
        public int id_estudiante { get; set; }
        public int id_periodo { get; set; }
        public string estado { get; set; } = "";

        // NAV: una matrícula tiene muchos detalles
        public ICollection<Detalle_Matricula> Detalles { get; set; }
            = new List<Detalle_Matricula>();
    }
}
