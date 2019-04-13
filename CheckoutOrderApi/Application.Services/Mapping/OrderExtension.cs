using System;
using System.Collections.Generic;
using System.Linq;
using Application.Dto;
using Domain.Model;

namespace Application.Services.Mapping
{
    public static class OrderExtension
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                CustomerId = order.CustomerId,
                DateCreated = order.DateCreated,
                DateModified = order.DateModified,
                Id = order.Id,
                OrderItems = ToDtoOrderItems(order.OrderItems)
            };
        }

        public static List<OrderDto> ToDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(x => x.ToDto()).ToList();
        }

        private static OrderItemDto ToDtoOrderItem(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ItemId = orderItem.ItemId,
                ItemName = orderItem.Item.Name,
                Quantity = orderItem.Quantity
            };
        }

        private static List<OrderItemDto> ToDtoOrderItems(List<OrderItem> orderItems)
        {
            return orderItems.Select(x => x.ToDtoOrderItem()).ToList();
        }
    }
}
