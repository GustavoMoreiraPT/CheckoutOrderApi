using Domain.Model;

namespace Domain.Core
{
    public interface IUnitOfWork
    {
        IRepository<Customer> CustomerRepository { get; }

        IRepository<Order> OrderRepository { get; }

        IRepository<OrderItem> OrderItemRepository { get; }

        IRepository<Item> ItemRepository { get; }

        void Save();

        void SeedItemsData();
    }
}
