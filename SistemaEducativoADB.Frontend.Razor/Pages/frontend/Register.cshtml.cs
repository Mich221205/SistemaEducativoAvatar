using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativoADB.Frontend.Razor.Pages.frontend
{
    public class RegisterModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;

        public RegisterModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [BindProperty] public string Nombre { get; set; } = string.Empty;
        [BindProperty] public string Email { get; set; } = string.Empty;
        [BindProperty] public string Password { get; set; } = string.Empty;
        [BindProperty] public string ConfirmPassword { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Password != ConfirmPassword)
            {
                Mensaje = "Las contraseñas no coinciden.";
                return Page();
            }

            var dto = new UsuarioRegisterDto
            {
                nombre = Nombre,
                email = Email,
                contrasena = Password,
                id_rol = 3 // por defecto Estudiante
            };

            var result = await _usuarioService.RegisterAsync(dto);

            if (result)
            {
                // Redirigir al login tras registrarse
                return RedirectToPage("/Frontend/Login");
            }

            Mensaje = "Error al registrar el usuario.";
            return Page();
        }
    }
}

