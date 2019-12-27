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
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _iOrderService;

        public OrderController(IOrderService iorderservice)
        {
            _iOrderService = iorderservice;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddOrder(CartViewModel cartViewModel)
        {
            var id = await _iOrderService.AddOrder(cartViewModel.OrderViewModel);
            await _iOrderService.AddOrderDetail(cartViewModel.listorderdetailvm, id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderList()
        {
            var order = await _iOrderService.GetOrderList();
            return new ObjectResult(order);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderDetailList(int id)
        {
            var vm = await _iOrderService.GetOrderDetail(id);
            return new ObjectResult(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(OrderViewModel vm)
        {
            await _iOrderService.UpdateOderStatus(vm);
            return Ok();
        }
    }
}