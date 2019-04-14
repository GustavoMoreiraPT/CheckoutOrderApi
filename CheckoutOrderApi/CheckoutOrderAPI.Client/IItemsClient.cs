using CheckoutOrderAPI.Client.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutOrderAPI.Client
{
    public interface IItemsClient
    {
        Task<List<ItemDto>> GetAllItemsAsync();
    }
}
