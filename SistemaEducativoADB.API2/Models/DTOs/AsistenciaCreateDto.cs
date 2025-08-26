using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class AsistenciaCreateDto
    {
        public int IdGrupo { get; set; }
        public int IdProfesor { get; set; }
        public int IdEstudiante { get; set; }
        public bool asistencia { get; set; }
        public DateTime? Fecha { get; set; } 
    }

}
