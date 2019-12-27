using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
        }

        public Comment(int id, Guid userId, string content, int productId, int rating, DateTime dateReview)
        {
            Id = id;
            UserId = userId;
            Content = content;
            ProductId = productId;
            Rating = rating;
            DateReview = dateReview;
        }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Rating { get; set; }
        public DateTime DateReview { get; set; }
    }
}
