using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime DateCreated { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
