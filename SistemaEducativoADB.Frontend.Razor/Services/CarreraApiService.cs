using SistemaEducativoADB.Frontend.Razor.Models;

public class CarreraApiService
{
    private readonly HttpClient _httpClient;

    public CarreraApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Carrera>> GetAllCarrerasAsync()
    {
        var response = await _httpClient.GetAsync("Carreras");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Carrera>>();
    }
}

