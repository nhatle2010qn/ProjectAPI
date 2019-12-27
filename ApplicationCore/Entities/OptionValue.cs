using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class OptionValue : BaseEntity
    {        
        public OptionValue(int id, int productId, string value)
        {
            Id = id;
            ProductId = productId;
            Value = value;
        }
        public Option Option { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Value { get; set; }
        
    }
}
