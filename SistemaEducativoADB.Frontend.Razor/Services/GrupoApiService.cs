using SistemaEducativoADB.Frontend.Razor.Models;

public class GrupoApiService
{
    private readonly HttpClient _httpClient;

    public GrupoApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Grupo>> GetAllGruposAsync()
    {
        var response = await _httpClient.GetAsync("Grupos");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Grupo>>();
    }
}

