using CheckoutOrderAPI.Client.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutOrderAPI.Client
{
    public interface IOrdersClient
    {
        Task<Uri> CreateOrderAsync(OrderDto order);

        Task UpdateOrderAsync(OrderDto order);

        Task DeleteOrderAsync(Guid orderId, Guid customerId);

        Task<List<OrderDto>> GetAllOrdersAsync(Guid customerId);

        Task<OrderDto> GetOrderById(Guid orderId, Guid customerId);
    }
}
