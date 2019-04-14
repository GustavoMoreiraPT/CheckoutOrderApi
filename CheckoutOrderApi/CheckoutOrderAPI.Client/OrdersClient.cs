using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CheckoutOrderAPI.Client.Dto;

namespace CheckoutOrderAPI.Client
{
    public class OrdersClient : IOrdersClient
    {
        private HttpClient httpClient;
        private const string baseUrl = "https://localhost:44392/";

        public OrdersClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<Uri> CreateOrderAsync(OrderDto order)
        {
            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync(
                $"{baseUrl}v1/orders", order);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task UpdateOrderAsync(OrderDto order)
        {
            HttpResponseMessage response = await this.httpClient.PutAsJsonAsync(
                $"{baseUrl}v1/orders/{order.Id}", order);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrderAsync(Guid orderId, Guid customerId)
        {
            HttpResponseMessage response = await this.httpClient.DeleteAsync(
                $"{baseUrl}v1/orders/{orderId}?customerId={customerId}");
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync(Guid customerId)
        {
            List<OrderDto> orders = new List<OrderDto>();

            HttpResponseMessage response = await this.httpClient.GetAsync($"{baseUrl}v1/orders?customerId={customerId}");
            if (response.IsSuccessStatusCode)
            {
                orders = await response.Content.ReadAsAsync<List<OrderDto>>();
            }
            return orders;
        }

        public async Task<OrderDto> GetOrderById(Guid orderId, Guid customerId)
        {
            OrderDto order = null;

            HttpResponseMessage response = await this.httpClient.GetAsync($"{baseUrl}v1/orders/{orderId}?customerId={customerId}");
            if (response.IsSuccessStatusCode)
            {
                order = await response.Content.ReadAsAsync<OrderDto>();
            }
            return order;
        }
    }
}
