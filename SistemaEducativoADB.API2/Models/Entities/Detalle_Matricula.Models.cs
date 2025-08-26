namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Detalle_Matricula
    {
        public int IdDetalle { get; set; }
        public int IdMatricula { get; set; }
        public int IdGrupo { get; set; }
        public decimal? Nota { get; set; }
        public string Condicion { get; set; } = "";

        public Matricula? Matricula { get; set; }
        public Grupo? Grupo { get; set; }
    }
}
