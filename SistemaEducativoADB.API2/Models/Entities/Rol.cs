namespace SistemaEducativoADB.API2.Models.Entities
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        // Relación inversa con usuarios
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}

