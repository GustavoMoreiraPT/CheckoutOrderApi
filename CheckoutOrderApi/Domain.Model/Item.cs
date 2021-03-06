﻿using System;

namespace Domain.Model
{
    public class Item : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}
