using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativo.Frontend.Pages.frontend.administrador
{
    public class ActivarCuentasModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public ActivarCuentasModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public List<Usuario> Usuarios { get; set; } = new();

        [BindProperty] public int UsuarioId { get; set; }
        [BindProperty] public bool NuevoEstado { get; set; }
        [BindProperty] public int NuevoRol { get; set; }

        public async Task OnGetAsync()
        {
            Usuarios = await _usuarioService.GetInactivosAsync() ?? new List<Usuario>();
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync()
        {
            await _usuarioService.CambiarEstadoAsync(UsuarioId, true);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCambiarRolAsync()
        {
            await _usuarioService.CambiarRolAsync(UsuarioId, NuevoRol);
            return RedirectToPage();
        }
    }
}
