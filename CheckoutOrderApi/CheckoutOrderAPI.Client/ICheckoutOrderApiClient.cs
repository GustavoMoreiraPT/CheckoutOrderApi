namespace CheckoutOrderAPI.Client
{
    public interface ICheckoutOrderApiClient
    {
        IItemsClient ItemsClient { get; }

        IOrdersClient OrdersClient { get; }
    }
}
