using FuraFila.Repository.SQlite.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Repository.SQlite.Seeds
{
    public static class DataSeed
    {
        public static void Seed(AppDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            if (!dbContext.Sellers.Any())
            {
                dbContext.Sellers.Add(new Domain.Seller
                {
                    Active = true,
                    Name = "Seller sample",
                });

                dbContext.Customers.Add(new Domain.Customer
                {
                    Name = "Customer sample"
                });

                dbContext.SaveChanges();
            }
        }
    }
}
