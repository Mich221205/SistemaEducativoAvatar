using SistemaEducativo.Frontend.Models;

namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class Grupo
    {
        public int IdGrupo { get; set; }        
        public int IdMateria { get; set; }     
        public int IdProfesor { get; set; }     
        public string GrupoNumero { get; set; } = ""; 
        public string Aula { get; set; } = "";        
        public int CupoMax { get; set; }              
        public MateriaViewModel? Materia { get; set; }
        public ProfesorViewModel? Profesor { get; set; }
    }
}



