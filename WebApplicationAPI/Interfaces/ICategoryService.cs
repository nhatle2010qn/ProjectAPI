using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoryList();
        Task<CategoryViewModel> GetById(int id);
        Task Delete(int id);
        Task Add(CategoryViewModel categoryViewModel);
        Task Update(CategoryViewModel categoryViewModel);
    }
}
