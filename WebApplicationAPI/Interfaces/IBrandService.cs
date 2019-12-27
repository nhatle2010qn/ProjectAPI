using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandViewModel>> GetBrandList();
        Task<BrandViewModel> GetById(int id);
        Task Delete(int id);
        Task Add(BrandViewModel brandViewModel);
        Task Update(BrandViewModel brandViewModel);
    }
}
