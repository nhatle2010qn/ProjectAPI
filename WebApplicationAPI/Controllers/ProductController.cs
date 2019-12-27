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
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;

        public ProductController(IProductService productService, ICommentService commentService)
        {
            _productService = productService;
            _commentService = commentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductList()
        {
            var products = await _productService.GetProducts();
            if (products != null)
            {
                foreach (var item in products)
                {
                    var rating = await _commentService.getRating(item.Id);
                    item.Rating = rating.Rating;
                }
            }
            return new OkObjectResult(products);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductListPaging(int page, int size, string search)
        {
            var products = await _productService.GetProductsPagination(page,size,search);
            return new OkObjectResult(products);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetHotSeller()
        {
            var products = await _productService.GetHotSellerProduct();
            if (products != null)
            {
               foreach(var item in products)
                {
                    var rating = await _commentService.getRating(item.Id);
                    item.Rating = rating.Rating;
                }
            }
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            return Ok(product);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(ProductViewModel vm)
        {
            if(vm.Id != 0)
            {
                await _productService.Update(vm);
            }
            else
            {
                await _productService.Add(vm);
            }
            return new OkObjectResult("Save Product Successfully");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByCategoryPaging(int page, int size, string search,string idCategory, string idBrand, int stPrice, int enPrice)
        {
            var items = await _productService.GetProductsByCategoryAndBrandPaging(page, size, search, idCategory, idBrand, stPrice, enPrice);
            return Ok(items);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductRelated(int id)
        {
            var products = await _productService.GetProductRelated(id);
            if (products != null)
            {
                foreach (var item in products)
                {
                    var rating = await _commentService.getRating(item.Id);
                    item.Rating = rating.Rating;
                }
            }
            return Ok(products);
        }         
        
    }
}