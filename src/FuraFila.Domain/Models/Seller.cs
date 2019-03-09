using FuraFila.Domain.Infrastructure;
using FuraFila.Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Models
{
    public class Seller : IEntity<string>, IEntityCreated, IEntityCreatedBy
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public List<Order> Orders { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }
    }
}
