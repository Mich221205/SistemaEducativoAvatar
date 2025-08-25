namespace SistemaEducativoADB.API2.Models.Entities
{
    public class DetallePago
    {
        public int IdDetallePago { get; set; }  // id_detalle_pago (PK)
        public int IdPago { get; set; }         // id_pago (FK -> PAGOS)
        public int IdMatricula { get; set; }    // id_matricula (FK -> MATRICULAS)
        public Pago? Pago { get; set; }
        public Matricula? Matricula { get; set; }
    }
}
