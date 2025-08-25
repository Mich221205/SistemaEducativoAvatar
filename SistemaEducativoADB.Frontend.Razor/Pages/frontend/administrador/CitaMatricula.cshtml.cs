using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaEducativo.Frontend.Pages.frontend.administrador
{
    public class CitaMatriculaModel : PageModel
    {
        [BindProperty] public int Anio { get; set; }
        [BindProperty] public string Periodo { get; set; } = string.Empty;
        [BindProperty] public DateTime Fecha { get; set; }
        [BindProperty] public TimeSpan Hora { get; set; }

        public void OnGet()
        {
            // Aquí cargarías las citas ya existentes desde la BD
        }

        public IActionResult OnPost()
        {
            // Aquí guardarías la cita nueva en la BD
            TempData["Mensaje"] = "Cita de matrícula guardada correctamente ✅";
            return RedirectToPage("/Frontend/administrador/CitaMatricula");
        }
    }
}
