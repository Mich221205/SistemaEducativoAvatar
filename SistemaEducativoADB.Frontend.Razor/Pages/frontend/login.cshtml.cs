using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativo.Frontend.Pages.Frontend
{
    public class LoginModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public LoginModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuario = await _usuarioService.LoginAsync(Email, Password);

            if (usuario != null)
            {
                // Guardar en sesión HTTPS y poder obtenerlos en otros lugares
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                HttpContext.Session.SetInt32("IdRol", usuario.id_rol);

                //  Redirección según rol 1=Admin, 2=Profesor, 3=Estudiante
                return usuario.id_rol switch
                {
                    1 => RedirectToPage("/frontend/administrador/Index_administrador"),
                    2 => RedirectToPage("/frontend/profesor/Index_Profesor"),
                    3 => RedirectToPage("/frontend/estudiantes/Index_estudiante"),
                    _ => RedirectToPage("/Error")
                };
            }

            Mensaje = "Correo o contraseña incorrectos.";
            return Page();
        }
    }
}

