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
    public class BrandService : IBrandService
    {
        private readonly IAsyncRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;
        public BrandService(IAsyncRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task Add(BrandViewModel brandViewModel)
        {
            var item = _mapper.Map<Brand>(brandViewModel);
            await _brandRepository.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var item = await _brandRepository.GetByIdAsync(id);
            await _brandRepository.DeleteAsync(item);
        }

        public async Task<BrandViewModel> GetById(int id)
        {
            var item = await _brandRepository.GetByIdAsync(id);
            return _mapper.Map<BrandViewModel>(item);
        }

        public async Task<List<BrandViewModel>> GetBrandList()
        {
            var list = await _brandRepository.ListAllAsync();
            return _mapper.Map<List<BrandViewModel>>(list);
        }

        public async Task Update(BrandViewModel BrandViewModel)
        {
            var item = _mapper.Map<Brand>(BrandViewModel);
            await _brandRepository.UpdateAsync(item);
        }
    }
}

