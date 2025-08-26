using SistemaEducativoADB.API2.Models.Entities;

namespace SistemaEducativoADB.API.Models.DTOs
{
    public class Detalle_PagosCreateDto
    {
        public int id_pago { get; set; }       // FK a PAGOS.id_pago
        public int id_matricula { get; set; }  // FK a MATRICULAS.id_matricula
    }
}