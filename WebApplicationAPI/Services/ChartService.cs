using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.Extensions;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;

namespace WebApplicationAPI.Services
{
    public class ChartService : IChartService
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Order_Detail> _orderDetailRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
   

        public ChartService(IAsyncRepository<Product> productRepository, IAsyncRepository<Order> orderRepository, IAsyncRepository<Order_Detail> orderDetailRepository, IAsyncRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ChartViewModel>> GetChartByCategory(string sortDate = "week")
        {
            var itemOrder = await _orderRepository.ListAllAsync();
            var itemDetail = await _orderDetailRepository.ListAllAsync();
            var itemProduct = await _productRepository.ListAllAsync();
            var itemCategory = await _categoryRepository.ListAllAsync();
            if (!String.IsNullOrEmpty(sortDate))
            {
                if (sortDate == "week")
                {
                    var startWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    itemOrder = itemOrder.Where(d => d.OrderDate >= startWeek).ToList();
                }
                if (sortDate == "month")
                {
                    itemOrder = itemOrder.Where(d => d.OrderDate.Month == DateTime.Now.Month).ToList();
                }
                if (sortDate == "year")
                {
                    itemOrder = itemOrder.Where(d => d.OrderDate.Year == DateTime.Now.Year).ToList();
                }
            }
           
            var item = (from o in itemOrder
                       join de in itemDetail
                       on o.Id equals de.Id
                       join pro in itemProduct
                       on de.ProductId equals pro.Id
                       join ca in itemCategory
                       on pro.CategoryId equals ca.Id                       
                       select new
                       {
                           categoryName = ca.Name,
                           count = de.Quantity
                       }).ToList();
            string category = null;
            List<ChartViewModel> listchart = new List<ChartViewModel>();        
            for (int i = 0; i < item.Count(); i++)
            {
                if(i == 0)
                {
                    ChartViewModel chart = new ChartViewModel();
                    category = item[i].categoryName;
                    chart.CategoryName = category;
                    foreach(var cha in item)
                    {
                        if(cha.categoryName == category)
                        {
                            chart.Count += cha.count;
                        }
                    }
                    listchart.Add(chart);
                }
                if (item[i].categoryName != category)
                {
                    ChartViewModel chart = new ChartViewModel();
                    category = item[i].categoryName;
                    chart.CategoryName = category;
                    foreach (var cha in item)
                    {
                        if (cha.categoryName == category)
                        {
                            chart.Count += cha.count;
                        }
                    }
                    listchart.Add(chart);
                }              
                
               
            }
            return listchart;
        }

        public async Task<List<ChartViewModel>> GetTopProductByCategory(int idCategory)
        {
            var item = await _productRepository.ListAllAsync();
            item = item.OrderByDescending(n => n.CountOrder)
                       .Take(5).ToList();
            
            List<ChartViewModel> chartList = new List<ChartViewModel>();
            for(var i = 0; i < item.Count(); i++)
            {
                ChartViewModel chart = new ChartViewModel()
                {
                    ProductName = item[i].Name,
                    Count = item[i].CountOrder
                };
                chartList.Add(chart);
            }
            return chartList;
        }


    }
}
