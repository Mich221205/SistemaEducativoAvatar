using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace SistemaEducativo.Frontend.Pages.frontend.profesor
{
    public class RegistrarNotasModel : PageModel
    {
        private readonly IHttpClientFactory _http;
        public RegistrarNotasModel(IHttpClientFactory http) => _http = http;

        [BindProperty(SupportsGet = true)]
        public int? IdGrupo { get; set; }

        public void OnGet() { }

        // GET /frontend/profesor/RegistrarNotas?handler=Grupos
        public async Task<IActionResult> OnGetGruposAsync()
        {
            var api = _http.CreateClient("ApiClient"); // BaseAddress = https://.../api/
            var res = await api.GetAsync("Notas/grupos");
            var txt = await res.Content.ReadAsStringAsync();
            // si viene vacío, devuelve []
            if (string.IsNullOrWhiteSpace(txt)) txt = "[]";
            return StatusCode((int)res.StatusCode, txt);
        }

        // GET /frontend/profesor/RegistrarNotas?handler=PorGrupo&idGrupo=123
        public async Task<IActionResult> OnGetPorGrupoAsync(int idGrupo)
        {
            var api = _http.CreateClient("ApiClient"); // BaseAddress termina en /api/
            var res = await api.GetAsync($"Notas/por-grupo/{idGrupo}");
            var txt = await res.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(txt)) txt = "[]";
            return StatusCode((int)res.StatusCode, txt);
        }

        // POST /frontend/profesor/RegistrarNotas?handler=Guardar&idGrupo=123
        public async Task<IActionResult> OnPostGuardarAsync(int idGrupo, [FromBody] object body)
        {
            var api = _http.CreateClient("ApiClient");
            var content = new StringContent(body?.ToString() ?? "[]", Encoding.UTF8, "application/json");
            var res = await api.PostAsync($"Notas/guardar-por-grupo/{idGrupo}", content);
            var txt = await res.Content.ReadAsStringAsync();
            // si el API no manda cuerpo en 204, devolvemos cadena vacía
            return StatusCode((int)res.StatusCode, string.IsNullOrEmpty(txt) ? "" : txt);
        }
    }
}
    

