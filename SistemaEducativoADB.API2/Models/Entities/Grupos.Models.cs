namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Grupo
    {
        public int IdGrupo { get; set; }        // id_grupo 
        public int IdMateria { get; set; }      // id_materia 
        public int IdProfesor { get; set; }     // id_profesor
        public string GrupoNumero { get; set; } = ""; // grupo_numero 
        public string Aula { get; set; } = "";        // aula 
        public int CupoMax { get; set; }              // cupo_max
        public Materia? Materia { get; set; }
        public Profesor? Profesor { get; set; }

        public ICollection<Detalle_Matricula> Detalles { get; set; }
            = new List<Detalle_Matricula>();
    }
}
