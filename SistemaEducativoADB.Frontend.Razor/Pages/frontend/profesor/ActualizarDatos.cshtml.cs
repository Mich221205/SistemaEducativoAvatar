using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativoADB.Frontend.Razor.Models;
using SistemaEducativoADB.Frontend.Razor.Services;
using System.Threading.Tasks;

namespace SistemaEducativo.Frontend.Pages.frontend.profesor
{
    public class ActualizarDatosModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        [BindProperty]
        public ActualizarProfesorDto DatosProfesor { get; set; } = new();

        public ActualizarDatosModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET → precargar los datos
        public async Task<IActionResult> OnGetAsync()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
            {
                return RedirectToPage("/frontend/Login");
            }

            var datos = await _usuarioService.GetDatosProfesorAsync(idUsuario.Value);
            if (datos != null)
            {
                DatosProfesor = datos;
            }

            return Page();
        }

        // POST → actualizar datos
        public async Task<IActionResult> OnPostAsync()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
            {
                return RedirectToPage("/frontend/Login");
            }

            bool ok = await _usuarioService.ActualizarDatosProfesorAsync(idUsuario.Value, DatosProfesor);
            if (ok)
            {
                TempData["Mensaje"] = "Datos actualizados correctamente";
            }
            else
            {
                TempData["Mensaje"] = "Error al actualizar los datos";
            }

            return Page();
        }
    }
}

