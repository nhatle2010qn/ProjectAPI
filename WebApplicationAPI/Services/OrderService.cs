using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Order_Detail> _orderDetailRepository;
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly UserManager<User> _userManager;
        public OrderService(IAsyncRepository<Order> orderRepository, IAsyncRepository<Order_Detail> orderdetailRepository, IAsyncRepository<Product> productRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderdetailRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public async Task<int> AddOrder(OrderViewModel vm)
        {
            Order order = new Order(vm.Id,vm.CouponId, DateTime.Now, vm.Total, OrderStatus.Waiting, vm.UserId);
            var or = await _orderRepository.AddAsync(order);
            return or.Id;
        }

        public async Task AddOrderDetail(List<OrderDetailViewModel> list, int id)
        {
            for(int i =0; i< list.Count(); i++)
            {
                Order_Detail order_dt = new Order_Detail(id, list[i].ProductId, list[i].Quantity);
                await _orderDetailRepository.AddAsync(order_dt);
                var product = await _productRepository.GetByIdAsync(list[i].ProductId);
                product.CountOrder += list[i].Quantity;
                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task<List<OrderViewModel>> GetOrderList()
        {
            var items = await _orderRepository.ListAllAsync();
            List<OrderViewModel> order = new List<OrderViewModel>();
            foreach(var item in items)
            {
                var username = await _userManager.FindByIdAsync(item.UserId.ToString());
                OrderViewModel vm = new OrderViewModel()
                {
                    Id = item.Id,
                    CouponId = item.CouponId,
                    OrderDate = item.OrderDate,
                    Total = item.Total,
                    UserId = item.UserId,
                    UserName = username.Name,
                    Status = item.Status.ToString()
                };
                order.Add(vm);
            }
                      
            return order;
        }

        public async Task<List<OrderDetailViewModel>> GetOrderDetail(int id)
        {
            var items = await _orderDetailRepository.ListAllAsync();
            var orderDt = items.Where(o => o.Id == id);
            List<OrderDetailViewModel> order = new List<OrderDetailViewModel>();
            foreach (var item in orderDt)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                OrderDetailViewModel detail = new OrderDetailViewModel()
                {
                    OrderId = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = product.Name
                };
                order.Add(detail);
            }      
            return order;
        }

        public async Task UpdateOderStatus(OrderViewModel vm)
        {
            OrderStatus status = (OrderStatus)Enum.Parse(typeof(OrderStatus), vm.Status);
            Order order = new Order()
            {
                Id = vm.Id,
                CouponId = vm.CouponId,
                OrderDate = vm.OrderDate,
                Total = vm.Total,
                Status = status,
                UserId = vm.UserId
            };
            await _orderRepository.UpdateAsync(order);

        }
    }
}
