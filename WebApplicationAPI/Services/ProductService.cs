using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Services    
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Product> _productRepository;
        public ProductService(IAsyncRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProductViewModel productViewModel)
        {
            Product product = _mapper.Map<Product>(productViewModel);
            await _productRepository.AddAsync(product);
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            var item = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductViewModel>(item);
            
        }

        public async Task<List<ProductViewModel>> GetHotSellerProduct()
        {
            var item = await _productRepository.ListAllAsync();
            item = item.OrderBy(n => n.CountOrder)
                       .Take(5).ToList();
            var vm = item.Select(i => new ProductViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                MainUrl = i.MainUrl,
                Price = i.Price,
                BrandId = i.BrandId,
                Image1 = i.Image1,
                Image2 = i.Image2,
                Image3 = i.Image3,
                CountOrder = i.CountOrder
            }).ToList();
            return vm;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var item = await _productRepository.ListAllAsync();
            var vm = item.Select(i => new ProductViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                MainUrl = i.MainUrl,
                Price = i.Price,
                BrandId = i.BrandId,
                Image1 = i.Image1,
                Image2 = i.Image2,
                Image3 = i.Image3,
                CountOrder = i.CountOrder
            }).ToList();
            return vm;
        }

        public async Task<ProductPaginationViewModel> GetProductsByCategoryAndBrandPaging(int page, int size, string search, string category, string brand, int stPrice, int enPrice)
        {
            var listPr = await _productRepository.ListAllAsync();
            var list = new List<Product>();
            listPr = listPr.Where(i => i.Price > Convert.ToDouble(stPrice) && i.Price < Convert.ToDouble(enPrice)).ToList();
            if (!String.IsNullOrEmpty(category))
            {
                List<string> listCategory = category.Split(new char[] { ',' }).ToList();
                foreach (var item in listCategory)
                {
                    var i = Int32.Parse(item);
                    list.AddRange(listPr.Where(p => p.CategoryId == i));
                }
            }
            if (!String.IsNullOrEmpty(brand))
            {
                List<string> listBrand = brand.Split(new char[] { ',' }).ToList();
                foreach (var item in listBrand)
                {
                    var j = Int32.Parse(item);
                    list.AddRange(listPr.Where(p => p.BrandId == j));
                }
            }
            if (String.IsNullOrEmpty(category) && String.IsNullOrEmpty(brand))
            {
                list = listPr;
            }
            var vm = list.Select(i => new ProductViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                MainUrl = i.MainUrl,
                Price = i.Price,
                BrandId = i.BrandId,
                Image1 = i.Image1,
                Image2 = i.Image2,
                Image3 = i.Image3,
                CountOrder = i.CountOrder
            }).ToList();       
            if (!String.IsNullOrEmpty(search))
            {
                vm = vm.Where(i => i.Name.Contains(search)).ToList();
            }
            int length = vm.Count();
            vm = vm.Skip((page - 1) * size).Take(size).ToList();
            ProductPaginationViewModel productPg = new ProductPaginationViewModel()
            {
                ProductList = vm,
                ProductLength = length
            };
            return productPg;
        }

        public async Task<ProductPaginationViewModel> GetProductsPagination(int page, int size, string search)
        {
            var item = await _productRepository.ListAllAsync();
            var vm = item.Select(i => new ProductViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                MainUrl = i.MainUrl,
                Price = i.Price,
                BrandId = i.BrandId,
                Image1 = i.Image1,
                Image2 = i.Image2,
                Image3 = i.Image3,
                CountOrder = i.CountOrder
            }).ToList();
            int length = vm.Count();
            if (!String.IsNullOrEmpty(search))
            {
                vm = vm.Where(i => i.Name.Contains(search)).ToList();
            }        
            vm = vm.Skip((page - 1) * size).Take(size).ToList();
            ProductPaginationViewModel productPg = new ProductPaginationViewModel()
            {
                ProductList = vm,
                ProductLength = length
            };
            return productPg;
        }

        public async Task Update(ProductViewModel productViewModel)
        {
            var item = _mapper.Map<Product>(productViewModel);
            await _productRepository.UpdateAsync(item);
        }

        public async Task<List<ProductViewModel>> GetProductRelated(int id)
        {
            var item = await _productRepository.ListAllAsync();
            var productitem = await _productRepository.GetByIdAsync(id);
            item = item.OrderBy(n => n.CountOrder)
                     .Take(5).ToList();
            item.Remove(productitem);        
            if(item.Count() > 4)
            {
                item.RemoveAt(item.Count() - 1);
            }
            var vm = item.Select(i => new ProductViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                MainUrl = i.MainUrl,
                Price = i.Price,
                BrandId = i.BrandId,
                Image1 = i.Image1,
                Image2 = i.Image2,
                Image3 = i.Image3,
                CountOrder = i.CountOrder
            }).ToList();
            return vm;
        }
    }
}
