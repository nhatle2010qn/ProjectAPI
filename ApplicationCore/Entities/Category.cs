using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Category : BaseEntity
    {
        public Category(int id, string name, int? parentid)
        {
            Id = id;
            Name = name;
            Parentid = parentid;
        }
        public string Name { get; set; }
        public int? Parentid { get; set; }
        public Category Parent { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Option> Options { get; set; }
    }
}
