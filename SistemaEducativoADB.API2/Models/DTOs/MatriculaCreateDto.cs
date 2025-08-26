namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class MatriculaCreateDto
    {
        public int id_estudiante { get; set; } // id_estudiante
        public int id_periodo { get; set; } // id_periodo
        public string? estado { get; set; } // estado (puede ser "Confirmada", "Pendiente".)
    }
}
