using System;
using System.Collections.Generic;
using Application.Dto;
using Application.Services.Interfaces;
using Domain.Core;

namespace Application.Services.Implementations
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<OrderDto> GetAll(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public OrderDto GetById(Guid orderId, Guid customerId)
        {
            throw new NotImplementedException();
        }

        public void Create(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto order)
        {
            throw new NotImplementedException();
        }
    }
}
