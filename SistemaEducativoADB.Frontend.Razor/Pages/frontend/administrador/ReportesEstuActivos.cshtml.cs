using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativo.Frontend.Services; // <- tu capa de servicios
using SistemaEducativo.Frontend.Models;   // <- tus DTOs o ViewModels
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativo.Frontend.Pages.frontend.administrador
{
    public class ReportesEstuActivosModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public ReportesEstuActivosModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Lista que se mostrará en la vista
        public List<EstudianteDto> EstudiantesActivos { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Llamar al servicio que trae estudiantes activos
            EstudiantesActivos = await _usuarioService.GetEstudiantesActivosAsync();
        }
    }
}
