using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var brands = await _brandService.GetBrandList();
            return Ok(brands);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetById(id);
            return Ok(brand);
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(BrandViewModel vm)
        {
            if (vm.Id != 0)
            {
                await _brandService.Update(vm);
            }
            else
            {
                await _brandService.Add(vm);
            }
            return Ok();
        }

    }
}