using SistemaEducativo.Frontend.Models;


namespace SistemaEducativo.Frontend.Services
{
    public class MateriaApiService : IMateriaApiService
    {
        private readonly HttpClient _httpClient;

        public MateriaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MateriaViewModel>> GetMateriasAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<MateriaViewModel>>("api/materias");
            return result ?? new List<MateriaViewModel>();
        }
    }
}
