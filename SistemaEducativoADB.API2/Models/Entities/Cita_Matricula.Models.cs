namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Cita_Matricula
    {
        public int IdCita { get; set; } 
        public int IdEstudiante { get; set; } 
        public int IdPeriodo { get; set; } 
        public DateTime FechaHora { get; set; } 

        
        public Estudiante Estudiante { get; set; }
        //public PeriodoLectivo PeriodoLectivo { get; set; }
    }
}

