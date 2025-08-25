namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Periodo_Lectivo
    {
        public int id_periodo { get; set; }       // PK
        public int anio { get; set; }             // año
        public string cuatrimestre { get; set; } = ""; // I, II, III, IV
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
    }
}
