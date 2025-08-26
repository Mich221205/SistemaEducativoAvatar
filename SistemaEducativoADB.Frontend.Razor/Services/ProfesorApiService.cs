using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;

public class ProfesorApiService
{
    private readonly HttpClient _httpClient;

    public ProfesorApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List< ProfesorViewModel>> GetAllProfesorsAsync()
    {
        var response = await _httpClient.GetAsync("Profesors");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ProfesorViewModel>>();
    }
}

