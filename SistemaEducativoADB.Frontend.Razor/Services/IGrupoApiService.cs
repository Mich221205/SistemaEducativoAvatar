using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaEducativoADB.Frontend.Razor.Models;


public interface IGrupoApiService
{
    Task<List<Grupo>> GetGruposAsync();
}

