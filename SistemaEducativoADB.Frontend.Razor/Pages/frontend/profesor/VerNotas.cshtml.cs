using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;

namespace SistemaEducativo.Frontend.Pages.frontend.profesor
{
    public class VerNotasModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VerNotasModel> _logger;

        public VerNotasModel(IHttpClientFactory httpClientFactory, ILogger<VerNotasModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public List<NotaCompleta> Notas { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                _logger.LogInformation("Inicio de carga de datos de notas.");

                var detalles = await _httpClient.GetFromJsonAsync<List<Detalle_Matricula>>("https://localhost:7076/api/detalle_matricula");
                _logger.LogInformation("Detalles de matrícula cargados: {Count}", detalles?.Count ?? 0);

                var matriculas = await _httpClient.GetFromJsonAsync<List<Matricula>>("https://localhost:7076/api/matriculas");
                _logger.LogInformation("Matrículas cargadas: {Count}", matriculas?.Count ?? 0);

                var estudiantes = await _httpClient.GetFromJsonAsync<List<Estudiante>>("https://localhost:7076/api/estudiantes");
                _logger.LogInformation("Estudiantes cargados: {Count}", estudiantes?.Count ?? 0);

                var usuarios = await _httpClient.GetFromJsonAsync<List<Usuario>>("https://localhost:7076/api/usuarios");
                _logger.LogInformation("Usuarios cargados: {Count}", usuarios?.Count ?? 0);

                var grupos = await _httpClient.GetFromJsonAsync<List<Grupo>>("https://localhost:7076/api/grupos");
                _logger.LogInformation("Grupos cargados: {Count}", grupos?.Count ?? 0);

                var materias = await _httpClient.GetFromJsonAsync<List<MateriaViewModel>>("https://localhost:7076/api/materias");
                _logger.LogInformation("Materias cargadas: {Count}", materias?.Count ?? 0);

                // Verificar que no sean null
                detalles = new List<Detalle_Matricula>();
                matriculas ??= new List<Matricula>();
                estudiantes ??= new List<Estudiante>();
                usuarios ??= new List<Usuario>();
                grupos ??= new List<Grupo>();
                materias = new List<MateriaViewModel>();

                // Combinar
                Notas = (from d in detalles
                         join m in matriculas on d.IdMatricula equals m.id_matricula
                         join e in estudiantes on m.id_estudiante equals e.IdEstudiante
                         join u in usuarios on e.IdUsuario equals u.IdUsuario
                         join g in grupos on d.IdGrupo equals g.IdGrupo
                         join mat in materias on g.IdMateria equals mat.IdMateria
                         select new NotaCompleta
                         {
                             Carnet = e.Carnet,
                             Nombre = u.nombre,
                             Materia = mat.Nombre,
                             Grupo = g.GrupoNumero,
                             Nota = d.Nota,
                             Condicion = d.Condicion
                         }).ToList();

                _logger.LogInformation("Notas combinadas: {Count}", Notas.Count);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cargando datos de notas.");
            }
        }

    }
}
