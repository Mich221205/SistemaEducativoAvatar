using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class AsistenciaCreateDto
    {
        public int IdGrupo { get; set; }
        public int IdProfesor { get; set; }
        public int IdEstudiante { get; set; }
        public bool eAsistencia { get; set; }
        public DateTime? Fecha { get; set; } 
    }

}
