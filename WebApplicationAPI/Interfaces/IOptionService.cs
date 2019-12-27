using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface IOptionService
    {
        Task<List<OptionViewModel>> GetAllOptions(int category);
        Task<List<OptionValueViewModel>> GetAllOptionsValue(int productId);

    }
}
