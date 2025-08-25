using Microsoft.AspNetCore.Authentication.Cookies;

namespace SistemaEducativoADB.Frontend.Razor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Frontend/Login"; // Página de login
                    options.AccessDeniedPath = "/Frontend/AccesoDenegado"; // Página si no tiene permisos
                });


            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7076/api/");
            });


            builder.Services.AddScoped<CarreraApiService>();


            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/frontend/Login", ""); 
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


            app.UseAuthentication();
            app.UseAuthorization();

   

            app.MapRazorPages();

            app.Run();
        }
    }
}
