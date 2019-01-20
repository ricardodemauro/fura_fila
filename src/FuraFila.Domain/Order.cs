using FuraFila.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public bool Paid { get; set; }

        public DateTime Created { get; set; }

        public int SellerId { get; set; }

        public Seller Seller { get; set; }
    }
}
