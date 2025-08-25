using SistemaEducativo.Frontend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



    public interface IMateriaApiService
    {
        Task<List<MateriaViewModel>> GetMateriasAsync();
    }
