﻿using FuraFila.Domain.Infrastructure;
using System;

namespace FuraFila.Domain.Models
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }
    }
}