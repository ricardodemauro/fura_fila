using FuraFila.Domain;
using FuraFila.Domain.Models;
using FuraFila.Repository.EF.Configurations;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuraFila.Repository.EF
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        //AppDbContext()
        //{
        //    Database.EnsureCreated();
        //}

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new SellersConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemsConfiguration());

            //relations
            modelBuilder.Entity<Seller>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
