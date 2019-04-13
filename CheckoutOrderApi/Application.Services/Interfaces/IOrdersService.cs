using Application.Dto;
using System;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IOrdersService
    {
        List<OrderDto> GetAll(Guid customerId);

        OrderDto GetById(Guid orderId, Guid customerId);

        void Create(OrderDto order);

        void Update(OrderDto order);

        void Delete(OrderDto order);
    }
}
