using FuraFila.Domain;
using FuraFila.Repository.SQlite.Configurations;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;

namespace FuraFila.Repository.SQlite
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new SellersConfiguration());


            //relations
            modelBuilder.Entity<Seller>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("factory.db");
        //    //base.OnConfiguring(optionsBuilder);
        //}
    }
}
