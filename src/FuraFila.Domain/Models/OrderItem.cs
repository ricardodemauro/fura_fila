using FuraFila.Domain.Infrastructure;
using FuraFila.Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Models
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string PictureUrl { get; set; }

        public int Quantity { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}
