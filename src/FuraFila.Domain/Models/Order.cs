using FuraFila.Domain.Infrastructure;
using FuraFila.Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Models
{
    public class Order : IEntity<string>, IEntityCreated, IEntityCreatedBy
    {
        public string Id { get; set; }

        public decimal UnitPrice { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Table Number
        /// </summary>
        public string TableId { get; set; }

        /// <summary>
        /// Id on other platforms
        /// </summary>
        public string ExternalId { get; set; }

        public bool IsPaid { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public string SellerId { get; set; }

        public Seller Seller { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
