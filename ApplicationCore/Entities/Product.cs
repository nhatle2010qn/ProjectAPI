using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        public Product(int id, string name, string description, double price, string mainUrl, string image1, string image2, string image3, int categoryId, int? brandId, int countOrder)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            MainUrl = mainUrl;
            Image1 = image1;
            Image2 = image2;
            Image3 = image3;
            CategoryId = categoryId;
            BrandId = brandId;
            CountOrder = countOrder;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string MainUrl { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CountOrder { get; set; }
        public List<Order_Detail> Order_Details { get; set; }
        public List<Comment> Comments { get; set; }
        public List<OptionValue> OptionValues { get; set; }
    }
}
