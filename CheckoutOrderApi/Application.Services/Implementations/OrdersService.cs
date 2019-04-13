using System;
using System.Collections.Generic;
using System.Linq;
using Application.Dto;
using Application.Services.Interfaces;
using Domain.Core;
using Application.Services.Mapping;
using Domain.Model;

namespace Application.Services.Implementations
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.unitOfWork.SeedItemsData();
        }

        public List<OrderDto> GetAll(Guid customerId)
        {
           var customer = this.unitOfWork.CustomerRepository.GetById(customerId);

            if (customer == null)
            {
                throw new ArgumentNullException($"Customer with Id {customerId} does not exist");
            }

            var orders = this.unitOfWork.OrderRepository.GetAll().Where(x => x.CustomerId == customerId);

            return orders.ToDtoList();
        }

        public OrderDto GetById(Guid orderId, Guid customerId)
        {
            var order = this.unitOfWork.OrderRepository.GetById(orderId);

            if (order == null || order.CustomerId != customerId)
            {
                throw new ArgumentException
                    ($"Order with Id {orderId} does not exist for customer with Id {customerId}");
            }

            return order.ToDto();
        }

        public void Create(OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("Cannot process new order. Object is null");
            }

            var customer = this.unitOfWork.CustomerRepository.GetById(order.CustomerId);

            if (customer == null)
            {
                throw new ArgumentNullException
                    ($"Cannot create order for customer {order.CustomerId} because customer does not exist");
            }

            var newOrder = new Order();
            newOrder.Customer = customer;
            newOrder.CustomerId = order.CustomerId;
            newOrder.DateCreated = DateTime.UtcNow;
            newOrder.DateModified = DateTime.UtcNow;
            newOrder.Id = order.Id;
            newOrder.OrderItems = GenerateOrderItems(order, newOrder);

            this.unitOfWork.OrderRepository.Create(newOrder);

            this.unitOfWork.Save();
        }

        public void Delete(Guid orderId, Guid customerId)
        {
            var order = this.unitOfWork.OrderRepository.GetById(orderId);

            if (order == null || order.CustomerId != customerId)
            {
                throw new ArgumentNullException
                    ($"Cannot delete order {order} because does not exist for customer {customerId}");
            }

            this.unitOfWork.OrderRepository.Delete(order);
        }

        public void Update(OrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("Cannot process new order. Object is null");
            }

            var customer = this.unitOfWork.CustomerRepository.GetById(order.CustomerId);

            if (customer == null)
            {
                throw new ArgumentNullException
                    ($"Cannot create order for customer {order.CustomerId} because customer does not exist");
            }

            var existingOrder = this.unitOfWork.OrderRepository.GetById(order.Id);

            if (existingOrder == null)
            {
                throw new ArgumentNullException
                   ("Cannot update order because it does not exist");
            }

            existingOrder.DateModified = DateTime.UtcNow;
            existingOrder.OrderItems = GenerateOrderItems(order, existingOrder);

            this.unitOfWork.OrderRepository.Edit(existingOrder);

            this.unitOfWork.Save();
        }

        public List<ItemDto> GetOrderItems(Guid orderId, Guid customerId)
        {
            var order = this.unitOfWork.OrderRepository.GetById(orderId);

            if (order == null || order.CustomerId != customerId)
            {
                throw new ArgumentException
                   ($"Order with Id {orderId} does not exist for customer with Id {customerId}");
            }

            var itemsIds = order.OrderItems.Select(x => x.ItemId).ToList();

            var items = new List<Item>();

            foreach (var itemId in itemsIds)
            {
                var item = this.unitOfWork.ItemRepository.GetById(itemId);

                items.Add(item);
            }

            return items.ToDtoList();
        }

        private List<OrderItem> GenerateOrderItems(OrderDto order, Order newOrder)
        {
            var orderItems = new List<OrderItem>();

            foreach (var orderItem in order.OrderItems)
            {
                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ItemId = orderItem.ItemId,
                    OrderId = newOrder.Id,
                    Order = newOrder,
                    Quantity = orderItem.Quantity,
                    Item = this.unitOfWork.ItemRepository.GetById(orderItem.ItemId)
                });
            }

            return orderItems;
        }
    }
}
