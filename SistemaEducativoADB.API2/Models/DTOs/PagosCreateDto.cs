namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class PagosCreateDto
    {
        public int id_estudiante { get; set; }         // FK a ESTUDIANTE
        public decimal monto { get; set; }
        public DateTime fecha { get; set; }            // Fecha del pago
        public string estado { get; set; } = "Pendiente";   // "Pendiente" | "Pagado"
        public string metodo_pago { get; set; } = "";       // "Tarjeta" | "Transferencia" | "Efectivo"
    }
}
