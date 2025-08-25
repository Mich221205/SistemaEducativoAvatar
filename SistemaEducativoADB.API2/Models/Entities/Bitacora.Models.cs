namespace SistemaEducativoADB.API2.Models.Entities
{

    public class Bitacora
    {
        public int IdLog { get; set; }         
        public int IdUsuario { get; set; }     
        public required string Accion { get; set; }    
        public DateTime FechaHora { get; set; } 
        public string Ip { get; set; }       

     
        public required Usuario Usuario { get; set; }
    }
}

