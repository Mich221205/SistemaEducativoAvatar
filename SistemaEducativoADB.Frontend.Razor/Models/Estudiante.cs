namespace SistemaEducativoADB.Frontend.Razor.Models
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; } // id_estudiante
        public int IdUsuario { get; set; }    // id_usuario
        public string Carnet { get; set; }    // carnet
        public string Telefono { get; set; }  // telefono
        public string Direccion { get; set; } // direccion
        public int? IdCarrera { get; set; }   // id_carrera (puede ser nullable si no siempre hay carrera)


        public Usuario Usuario { get; set; }

    }
}

