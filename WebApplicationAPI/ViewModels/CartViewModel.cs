using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPI.ViewModels
{
    public class CartViewModel
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderDetailViewModel> listorderdetailvm { get; set; }
    }
}
