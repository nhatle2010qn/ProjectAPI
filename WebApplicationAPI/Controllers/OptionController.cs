using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Interfaces;

namespace WebApplicationAPI.Controllers
{
    public class OptionController : BaseApiController
    {
        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetOptionList(int categoryId)
        {
            var item = await _optionService.GetAllOptions(categoryId);
            return Ok(item);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetOptionValueList(int productId)
        {
            var item = await _optionService.GetAllOptionsValue(productId);
            return Ok(item);
        }
    }
}