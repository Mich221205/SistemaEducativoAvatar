using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;


public interface IEstudianteApiService
{
    Task<List<Estudiante>> GetEstudiantesAsync();
}

