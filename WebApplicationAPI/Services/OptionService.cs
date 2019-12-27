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
    public class OptionService : IOptionService
    {
        private readonly IAsyncRepository<Option> _optionRepository;
        private readonly IAsyncRepository<OptionValue> _optionValueRepository;
        private readonly IMapper _mapper;

        public OptionService(IAsyncRepository<Option> optionRepository, IMapper mapper, IAsyncRepository<OptionValue> optionValueRepository)
        {
            _optionRepository = optionRepository;
            _optionValueRepository = optionValueRepository;
            _mapper = mapper;
        }
        public async Task<List<OptionViewModel>> GetAllOptions(int category)
        {
            var item = await _optionRepository.ListAllAsync();
            item = item.Where(i => i.CategoryId == category).ToList();
            return _mapper.Map<List<OptionViewModel>>(item);
        }

        public async Task<List<OptionValueViewModel>> GetAllOptionsValue(int productId)
        {
            var item = await _optionValueRepository.ListAllAsync();
            item = item.Where(i => i.ProductId == productId).ToList();
            return _mapper.Map<List<OptionValueViewModel>>(item);
        }
    }
}
