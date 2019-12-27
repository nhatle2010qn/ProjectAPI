using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Coupon : BaseEntity
    {
        public Coupon(int id, string name, string description, DateTime dateStarted, DateTime dateEnded, bool active, int value)
        {
            Id = id;
            Name = name;
            Description = description;
            DateStarted = dateStarted;
            DateEnded = dateEnded;
            Active = active;
            Value = value;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public bool Active { get; set; }
        public int Value { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
