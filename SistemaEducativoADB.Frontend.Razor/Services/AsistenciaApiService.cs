using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;

public class AsistenciaApiService
{
    private readonly HttpClient _httpClient;

    public AsistenciaApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Asistencia>> GetAllAsistenciasAsync()
    {
        var response = await _httpClient.GetAsync("Asistencias");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Asistencia>>();
    }
}

