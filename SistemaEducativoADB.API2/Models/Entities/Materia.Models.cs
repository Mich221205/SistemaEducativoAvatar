namespace SistemaEducativoADB.API2.Models.Entities
{

    public class Materia
    {
        public int IdMateria { get; set; } // id_materia
        public string Codigo { get; set; } // codigo
        public string Nombre { get; set; } // nombre
        public int Creditos { get; set; }  // creditos
        public int? IdPlan { get; set; }   // id_plan (nullable por si no tiene plan asignado)
        public Plan_Estudio? PlanEstudio { get; set; }
    }
    }
