using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(int id, int? couponId, DateTime orderDate, double total, OrderStatus? status, Guid? userId )
        {
            Id = id;
            CouponId = couponId;
            OrderDate = orderDate;
            Total = total;
            UserId = userId;
            Status = status;
        } 
        public int? CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public OrderStatus? Status { get; set; }
        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Order_Detail> Order_Details { get; set; }
    }
}
