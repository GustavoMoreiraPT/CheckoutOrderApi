using System;
using System.Collections.Generic;

namespace Application.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}