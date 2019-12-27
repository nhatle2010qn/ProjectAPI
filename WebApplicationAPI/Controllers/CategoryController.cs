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
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetCategoryList();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = nameof(GetById))]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var categories = await _categoryService.GetById(id);
            return Ok(categories);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(CategoryViewModel vm)
        {
            if (vm.Id != 0)
            {
                await _categoryService.Update(vm);
            }
            else
            {
                await _categoryService.Add(vm);
            }
            return Ok();
        }

    }
}