using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class Detalle_MatriculaCreateDto
    {
        public int IdMatricula { get; set; }
        public int IdGrupo { get; set; }
        public decimal Nota { get; set; } = 0;
        public string Condicion { get; set; } = "Cursando";
    }
}
