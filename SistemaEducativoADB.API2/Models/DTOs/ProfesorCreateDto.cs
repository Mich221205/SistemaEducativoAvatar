using Microsoft.AspNetCore.Http.HttpResults;

namespace SistemaEducativoADB.API.Models.DTOs
{
    public class ProfesorCreateDto
    {
        public int IdUsuario { get; set; }  
        public string Cedula { get; set; } 
        public string Telefono { get; set; }
        public string CorreoPersonal { get; set; }
    }

}
