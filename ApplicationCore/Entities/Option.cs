using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Option : BaseEntity
    {
        public Option(int id, string name, int categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual List<OptionValue> OptionValues { get; set; }
    }
}
