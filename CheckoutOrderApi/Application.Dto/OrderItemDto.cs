using System;

namespace Application.Dto
{
    public class OrderItemDto
    {
        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}
