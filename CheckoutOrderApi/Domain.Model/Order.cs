using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
