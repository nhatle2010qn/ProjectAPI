using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface IChartService
    {
        Task<List<ChartViewModel>> GetTopProductByCategory(int idCategory);
        Task<List<ChartViewModel>> GetChartByCategory(string sortDate);
        
        
    }
}
