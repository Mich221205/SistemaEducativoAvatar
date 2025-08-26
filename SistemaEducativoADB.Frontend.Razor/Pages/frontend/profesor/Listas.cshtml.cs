using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaEducativo.Frontend.Pages.frontend.profesor
{
    public class ListasModel : PageModel
    {
        private readonly EstudianteApiService _apiService;

        public ListasModel(EstudianteApiService apiService)
        {
            _apiService = apiService;
        }

        public List<SistemaEducativoADB.Frontend.Razor.Models.Estudiante> estudiantes { get; set; } = new();


        public async Task OnGetAsync()
        {
            estudiantes = await _apiService.GetAllEstudiantesAsync();
        }
    }
}
