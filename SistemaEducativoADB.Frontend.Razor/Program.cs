using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaEducativoADB.Frontend.Razor.Services;


namespace SistemaEducativoADB.Frontend.Razor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cliente HTTP para la API
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7076/api/");
            });

            // Services propios
            builder.Services.AddScoped<CarreraApiService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();

            // Autenticación con cookies (si luego lo quieres combinar con roles)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Frontend/Login";
                    options.AccessDeniedPath = "/Frontend/AccesoDenegado";
                });

            // Razor Pages + ruta de login
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/frontend/login", "");
            });

            // Habilitar sesiones
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 🚀 IMPORTANTE: habilitar sesiones aquí, antes de Auth
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
