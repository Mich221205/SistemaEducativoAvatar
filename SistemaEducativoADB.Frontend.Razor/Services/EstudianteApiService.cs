using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;

public class EstudianteApiService
{
    private readonly HttpClient _httpClient;

    public EstudianteApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Estudiante>> GetAllEstudiantesAsync()
    {
        var response = await _httpClient.GetAsync("Estudiantes");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Estudiante>>();
    }
}

