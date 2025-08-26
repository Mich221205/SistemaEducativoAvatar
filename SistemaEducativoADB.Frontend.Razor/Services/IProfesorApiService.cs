using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaEducativo.Frontend.Models;
using SistemaEducativoADB.Frontend.Razor.Models;


public interface IProfesorApiService
{
    Task<List<ProfesorViewModel>> GetProfesorsAsync();
}

