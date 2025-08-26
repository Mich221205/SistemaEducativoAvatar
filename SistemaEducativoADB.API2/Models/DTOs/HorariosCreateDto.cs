namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class HorariosCreateDto
    {
        public int id_grupo { get; set; }
        public string dia_semana { get; set; } = "";
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_fin { get; set; }
    }
}
