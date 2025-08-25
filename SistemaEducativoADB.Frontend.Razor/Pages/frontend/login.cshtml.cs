using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaEducativo.Frontend.Pages.Frontend
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Email == "admin@correo.com" && Password == "1234")
            {
                return RedirectToPage("/Index");
            }
            else
            {
                Mensaje = "Correo o contraseña incorrectos.";
                return Page();
            }
        }
    }
}
