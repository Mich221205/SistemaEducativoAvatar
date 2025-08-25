namespace SistemaEducativoADB.API.Models.DTOs
{
    public class MateriaCreateDto
    {
        public int IdMateria { get; set; } // id_materia
        public string Codigo { get; set; } // codigo
        public string nombre { get; set; } // nombre
        public int Creditos { get; set; }  // creditos
        public int? IdPlan { get; set; }
    }
}
