using FuraFila.Domain.Models;
using FuraFila.Repository.EF.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Repository.EF.Seeds
{
    public static class DataSeed
    {
        public static void Seed(AppDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            if (!dbContext.Sellers.Any())
            {
                dbContext.Sellers.Add(new Seller
                {
                    IsActive = true,
                    Name = "Seller sample",
                });

                dbContext.SaveChanges();
            }
        }
    }
}
