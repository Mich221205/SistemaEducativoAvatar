using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;


public interface IAsistenciaApiService
{
    Task<List<Asistencia>> GetAsistenciasAsync();
}

