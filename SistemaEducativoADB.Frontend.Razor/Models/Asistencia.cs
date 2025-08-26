using SistemaEducativo.Frontend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdGrupo { get; set; }
        public int IdProfesor { get; set; }
        public int IdEstudiante { get; set; }

        public bool asistencia { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;


        public Grupo? Grupo { get; set; }
        public ProfesorViewModel? Profesor { get; set; }
        public Estudiante? Estudiante { get; set; }
    }
}

