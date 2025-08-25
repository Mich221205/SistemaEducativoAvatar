namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Horario
    {
        public int IdHorario { get; set; }          // PK (IDENTITY)
        public int IdGrupo { get; set; }            // FK a GRUPOS
        public string DiaSemana { get; set; } = "";
        public TimeSpan HoraInicio { get; set; }    // time en SQL Server
        public TimeSpan HoraFin { get; set; }       // time en SQL Server
        public Grupo? Grupo { get; set; }
    }
}
