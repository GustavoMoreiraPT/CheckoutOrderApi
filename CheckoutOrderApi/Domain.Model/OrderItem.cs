using System;

namespace Domain.Model
{
    public class OrderItem : IEntity
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        public virtual Item Item { get; set; }

        public virtual Order Order { get; set; }
    }
}
