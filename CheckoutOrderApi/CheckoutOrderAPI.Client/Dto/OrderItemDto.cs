using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutOrderAPI.Client.Dto
{
    public class OrderItemDto
    {
        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}
