using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetProducts();
        Task<ProductViewModel> GetById(int id);
        Task Delete(int id);
        Task Add(ProductViewModel productViewModel);
        Task Update(ProductViewModel productViewModel);
        Task<ProductPaginationViewModel> GetProductsPagination(int page, int size, string search);
        Task<List<ProductViewModel>> GetHotSellerProduct();
        Task<List<ProductViewModel>> GetProductRelated(int id);
        Task<ProductPaginationViewModel> GetProductsByCategoryAndBrandPaging(int page, int size, string search, string category, string brand, int stPrice, int endPrice);
    }
}
