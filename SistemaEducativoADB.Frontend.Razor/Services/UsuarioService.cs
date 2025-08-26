using System.Net.Http.Json;
using SistemaEducativoADB.Frontend.Razor.Models;

public class UsuarioService : IUsuarioService
{
    private readonly HttpClient _httpClient;

    public UsuarioService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<Usuario?> LoginAsync(string email, string contrasena)
    {
        var dto = new { email = email, contrasena = contrasena };

        var response = await _httpClient.PostAsJsonAsync("Usuario/login", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        return null;
    }

    public async Task<bool> RegisterAsync(UsuarioRegisterDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("Usuario", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Usuario>?> GetAllUsuariosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>("Usuario");
    }
    public async Task<List<Usuario>?> GetInactivosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>("Usuario/inactivos");
    }

    public async Task<bool> CambiarEstadoAsync(int idUsuario, bool nuevoEstado)
    {
        var response = await _httpClient.PutAsJsonAsync($"Usuario/cambiar-estado/{idUsuario}", nuevoEstado);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CambiarRolAsync(int idUsuario, int nuevoRol)
    {
        var response = await _httpClient.PutAsJsonAsync($"Usuario/cambiar-rol/{idUsuario}", nuevoRol);
        return response.IsSuccessStatusCode;
    }

}
