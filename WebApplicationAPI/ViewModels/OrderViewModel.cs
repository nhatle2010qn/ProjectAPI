using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPI.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int? CouponId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
}
