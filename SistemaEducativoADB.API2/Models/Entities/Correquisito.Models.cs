namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Correquisito
    {
        public int IdMateria { get; set; }          
        public int IdCorrequisito { get; set; }     

    
        public Materia Materia { get; set; }       
        public Materia CorrequisitoMateria { get; set; } 
    }
}

