using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPI.ViewModels
{
    public class ProductPaginationViewModel
    {
        public List<ProductViewModel> ProductList { get; set; }
        public int ProductLength { get; set; }
    }
}
