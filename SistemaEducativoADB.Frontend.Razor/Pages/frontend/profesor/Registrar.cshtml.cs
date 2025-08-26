using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativo.Frontend.Pages.frontend.profesor
{
    public class RegistrarModel : PageModel
    {
        private readonly EstudianteApiService _apiService;

    public RegistrarModel(EstudianteApiService apiService)
    {
        _apiService = apiService;
    }
    
    public List<SistemaEducativoADB.Frontend.Razor.Models.Estudiante> estudiantes { get; set; } = new();
    
    // ¡ESTAS SON LAS PROPIEDADES QUE TE FALTAN!
    [BindProperty(SupportsGet = true)]
    public int IdGrupo { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int IdProfesor { get; set; }
    
    public async Task OnGetAsync()
    {
        Console.WriteLine($"IdGrupo recibido: {IdGrupo}");
        Console.WriteLine($"IdProfesor recibido: {IdProfesor}");
        
        // Si no se proporcionan parámetros, usar valores por defecto válidos
        if (IdGrupo == 0)
        {
            IdGrupo = 1; // ID de un grupo que sepas que existe
            Console.WriteLine($"IdGrupo asignado por defecto: {IdGrupo}");
        }
        
        if (IdProfesor == 0)
        {
            IdProfesor = 1; // ID de un profesor que sepas que existe  
            Console.WriteLine($"IdProfesor asignado por defecto: {IdProfesor}");
        }
        
        estudiantes = await _apiService.GetAllEstudiantesAsync();
        
        foreach (var est in estudiantes)
        {
            if (est.Asistencia == null)
            {
                est.Asistencia = new Asistencia
                {
                    IdAsistencia = 0,
                    IdGrupo = IdGrupo,
                    IdProfesor = IdProfesor,
                    IdEstudiante = est.IdEstudiante,
                    asistencia = false
                };
                
                Console.WriteLine($"Asistencia creada - IdGrupo: {IdGrupo}, IdProfesor: {IdProfesor}, IdEstudiante: {est.IdEstudiante}");
            }
        }
    }
}
    }
   


