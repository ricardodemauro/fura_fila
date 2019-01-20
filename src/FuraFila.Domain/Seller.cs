using FuraFila.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain
{
    public class Seller : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public List<Order> Orders { get; set; }
    }
}
