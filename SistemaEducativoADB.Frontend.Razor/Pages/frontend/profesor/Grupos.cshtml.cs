using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaEducativo.Frontend.Pages.frontend.profesor
{
    public class GruposModel : PageModel
    {
        private readonly GrupoApiService _apiService;

        public GruposModel(GrupoApiService apiService)
        {
            _apiService = apiService;
        }

        public List<SistemaEducativoADB.Frontend.Razor.Models.Grupo> Grupos { get; set; } = new();


        public async Task OnGetAsync()
        {
            Grupos = await _apiService.GetAllGruposAsync();
        }
    }
}
