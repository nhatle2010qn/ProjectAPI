using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options)
                : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Detail> Order_Details { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order_Detail>()
                .HasKey(o => new { o.Id, o.ProductId });
            modelBuilder.Entity<Order>()
                .HasKey(o => new { o.Id});
            modelBuilder.Entity<Order_Detail>()
            .HasOne(p => p.Order)
            .WithMany(b => b.Order_Details)
            .HasForeignKey(p => p.Id)
            .HasConstraintName("ForeignKey_OrderDetail_Order");

            modelBuilder.Entity<OptionValue>()
                .HasKey(o => new { o.Id, o.ProductId });
            modelBuilder.Entity<OptionValue>()
        .HasOne(p => p.Option)
        .WithMany(b => b.OptionValues)
        .HasForeignKey(p => p.Id)
        .HasConstraintName("ForeignKey_OptionValue_Option");


        }
    }
}
