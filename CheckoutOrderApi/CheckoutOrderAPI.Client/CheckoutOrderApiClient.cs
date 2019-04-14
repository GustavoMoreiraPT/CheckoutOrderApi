using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutOrderAPI.Client
{
    public class CheckoutOrderApiClient : ICheckoutOrderApiClient
    {
        private readonly Lazy<IItemsClient> itemsClient;
        private readonly Lazy<IOrdersClient> ordersClient;

        public CheckoutOrderApiClient()
        {
            this.itemsClient = new Lazy<IItemsClient>(() => new ItemsClient());
            this.ordersClient = new Lazy<IOrdersClient>(() => new OrdersClient());
        }

        public IItemsClient ItemsClient => this.itemsClient.Value;

        public IOrdersClient OrdersClient => this.ordersClient.Value;
    }
}
