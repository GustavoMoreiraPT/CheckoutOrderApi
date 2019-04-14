using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CheckoutOrderAPI.Client.Dto;

namespace CheckoutOrderAPI.Client
{
    public class ItemsClient : IItemsClient
    {
        private HttpClient httpClient;
        private const string baseUrl = "https://localhost:44392/";

        public ItemsClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<List<ItemDto>> GetAllItemsAsync()
        {
            List<ItemDto> items = new List<ItemDto>();

            HttpResponseMessage response = await this.httpClient.GetAsync($"{baseUrl}v1/items");
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<ItemDto>>();
            }
            return items;
        }
    }
}
