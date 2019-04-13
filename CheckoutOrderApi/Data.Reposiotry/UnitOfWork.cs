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

        private bool IsDataSeeded = false;

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
            //Because current implementation is in memory, this method will be ignored.
        }

        public void SeedItemsData()
        {
            if (IsDataSeeded)
            {
                return;
            }

            IsDataSeeded = true;

            ItemRepository.Create(new Item
            {
                Id = Guid.Parse("14ad7315-dd47-462c-ae05-925289420250"),
                Name = "Ultra Music Festival",
                Category = "Eletronic Dance Music",
                Price = 299
            });

            this.itemRepository.Create(new Item
            {
                Id = Guid.Parse("89d6f5bf-48d5-47bf-863f-f8b99f78d4c7"),
                Name = "Tomorrowland",
                Category = "Eletronic Dance Music",
                Price = 699
            });

            this.itemRepository.Create(new Item
            {
                Id = Guid.Parse("bd88ce04-6e43-479e-a39c-2d95b77aa97f"),
                Name = "Creamfields London",
                Category = "Eletronic Dance Music",
                Price = 99
            });

            CustomerRepository.Create(new Customer
            {
                Id = Guid.Parse("8ffbacec-4ad8-4ef0-a016-1b90b35a37e9"),
                Email = "arminvanbuuren@netherlands.com",
                Password = "SunnyDays"
            });

            CustomerRepository.Create(new Customer
            {
                Id = Guid.Parse("eed9980b-f90c-47fb-8c2f-9708b0ec636c"),
                Email = "avicii@sweden.com",
                Password = "SOS"
            });
        }
    }
}
