using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativoADB.Frontend.Razor.Pages.Carreras
{
    public class IndexModel : PageModel
    {
        private readonly CarreraApiService _apiService;

        public IndexModel(CarreraApiService apiService)
        {
            _apiService = apiService;
        }

        public List<SistemaEducativoADB.Frontend.Razor.Models.Carrera> Carreras { get; set; } = new();


        public async Task OnGetAsync()
        {
            Carreras = await _apiService.GetAllCarrerasAsync();
        }
    }
}



