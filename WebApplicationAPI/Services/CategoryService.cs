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
    public class CategoryService : ICategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task Add(CategoryViewModel categoryViewModel)
        {
            var item = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var item = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(item);
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var item = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(item);
        }

        public async Task<List<CategoryViewModel>> GetCategoryList()
        {
            var list = await _categoryRepository.ListAllAsync();
            return _mapper.Map<List<CategoryViewModel>>(list);
        }

        public async Task Update(CategoryViewModel categoryViewModel)
        {
            var item = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.UpdateAsync(item);
        }
    }
}
