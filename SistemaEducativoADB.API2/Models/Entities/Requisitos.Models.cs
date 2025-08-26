namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Requisito
    {
        public int IdMateria { get; set; }     // id_materia
        public int IdRequisito { get; set; }   // id_requisito
        public Materia? Materia { get; set; }
        public Materia? MateriaRequisito { get; set; }
    }
}
