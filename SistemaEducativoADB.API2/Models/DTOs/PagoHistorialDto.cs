namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class PagoHistorialDto
    {
        public string CodigoPago { get; set; } = ""; // Generado, no existe en la BD
        public string Nombre { get; set; } = "";     // Viene de la tabla USUARIOS (join)
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "";
        public decimal MontoTotal { get; set; }      // Igual que Monto, pero renombrado
        public string MetodoPago { get; set; } = ""; // Igual que en la BD
    } 
}
