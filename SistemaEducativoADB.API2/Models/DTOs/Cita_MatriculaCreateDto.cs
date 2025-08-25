using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class CitaMatriculaCreateDto
    {
        public int IdEstudiante { get; set; }
        public int IdPeriodo { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
