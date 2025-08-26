namespace SistemaEducativoADB.API2.Models.DTOs
{
    public class ActualizarProfesorDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? CorreoPersonal { get; set; }
    }
}
