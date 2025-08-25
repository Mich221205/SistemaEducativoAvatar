using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaEducativo.Frontend.Pages.frontend.administrador
{
    public class AsignarMateriasProfesModel : PageModel
    {
        // ✅ Propiedades para usar con asp-for en la vista
        [BindProperty]
        public string Profesor { get; set; } = string.Empty;

        [BindProperty]
        public string Materia { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Aquí simulas guardar la asignación (por ahora solo feedback)
            TempData["Mensaje"] = $"Asignación realizada: {Profesor} → {Materia} ✅";
            return RedirectToPage("/Frontend/administrador/AsignarMateriasProfes");
        }
    }
}
