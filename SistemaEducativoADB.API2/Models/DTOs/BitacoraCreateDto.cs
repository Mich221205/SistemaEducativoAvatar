using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class BitacoraCreateDto
    {
        public int IdUsuario { get; set; }      
        public string Accion { get; set; }       
        public DateTime? FechaHora { get; set; } 
        public string Ip { get; set; }          
    }
}
