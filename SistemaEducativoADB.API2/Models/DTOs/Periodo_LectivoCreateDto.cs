namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class Periodo_LectivoCreateDto
    {
        public int anio { get; set; }                 // año (ej. 2025)
        public string cuatrimestre { get; set; } = ""; // I, II, III, IV
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
    }
}

