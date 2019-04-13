using Domain.Core;
using Domain.Model;
using System;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IRepository<Customer> customerRepository;
        private IRepository<Order> orderRepository;
        private IRepository<OrderItem> orderItemRepository;
        private IRepository<Item> itemRepository;

        public UnitOfWork()
        {

        }

        public IRepository<Customer> CustomerRepository
        {
            get
            {
                return this.customerRepository ?? (this.customerRepository = new GenericInMemoryRepository<Customer>());
            }
        }

        public IRepository<Order> OrderRepository
        {
            get
            {
                return this.orderRepository ?? (this.orderRepository = new GenericInMemoryRepository<Order>());
            }
        }

        public IRepository<OrderItem> OrderItemRepository
        {
            get
            {
                return this.orderItemRepository ?? (this.orderItemRepository = new GenericInMemoryRepository<OrderItem>());
            }
        }

        public IRepository<Item> ItemRepository
        {
            get
            {
                return this.itemRepository ?? (this.itemRepository = new GenericInMemoryRepository<Item>());
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
        }

        public void SeedItemsData()
        {
            ItemRepository.Create(new Item
            {
                Id = Guid.NewGuid(),
                Name = "Ultra Music Festival",
                Category = "Eletronic Dance Music",
                Price = 299
            });

            this.itemRepository.Create(new Item
            {
                Id = Guid.NewGuid(),
                Name = "Tomorrowland",
                Category = "Eletronic Dance Music",
                Price = 699
            });

            this.itemRepository.Create(new Item
            {
                Id = Guid.NewGuid(),
                Name = "Creamfields London",
                Category = "Eletronic Dance Music",
                Price = 99
            });
        }
    }
}
