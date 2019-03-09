using FuraFila.Domain.Infrastructure;
using FuraFila.Domain.Infrastructure.Entities;
using System;

namespace FuraFila.Domain.Models
{
    public class Customer : IEntity<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string AreaCode { get; set; }

        public string Phone { get; set; }
    }
}
