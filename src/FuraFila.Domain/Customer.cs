using FuraFila.Domain.Infrastructure;
using System;

namespace FuraFila.Domain
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
