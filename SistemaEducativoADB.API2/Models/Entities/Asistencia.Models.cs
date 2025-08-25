using SistemaEducativoADB.API2.Models.Entities;
using System.Text.RegularExpressions;

public class Asistencia
{
    public int IdAsistencia { get; set; }   
    public int IdGrupo { get; set; }
    public int IdProfesor { get; set; }
    public int IdEstudiante { get; set; }
    public bool eAsistencia { get; set; }  
    public DateTime Fecha { get; set; } = DateTime.Now;

    // Relaciones opcionales (si usas navegación)
    public Grupo Grupo { get; set; }
    public Profesor Profesor { get; set; }
    public Estudiante Estudiante { get; set; }
}
