namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Plan_Estudio
    {
        // PK 
        public int id_plan { get; set; }
        // FK a CARRERAS
        public int id_carrera { get; set; }
        public int anio_inicio { get; set; }
        public Carrera? Carrera { get; set; }
        public ICollection<Materia>? Materias { get; set; }
    }
}
