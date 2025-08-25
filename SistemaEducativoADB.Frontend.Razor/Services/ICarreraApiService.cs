using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaEducativoADB.Frontend.Razor.Models;


public interface ICarreraApiService
{
    Task<List<Carrera>> GetCarrerasAsync();
}

