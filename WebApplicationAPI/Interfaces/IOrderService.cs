using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(OrderViewModel vm);
        Task AddOrderDetail(List<OrderDetailViewModel> dtvm, int id);
        Task<List<OrderViewModel>> GetOrderList();
        Task<List<OrderDetailViewModel>> GetOrderDetail(int id);
        Task UpdateOderStatus(OrderViewModel vm);
    }
}
