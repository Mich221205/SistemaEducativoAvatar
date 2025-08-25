namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class Plan_EstudioCreateDto
    {
        // FK a CARRERAS
        public int id_carrera { get; set; }

        // Año de inicio del plan
        public int anio_inicio { get; set; }
    }
}
