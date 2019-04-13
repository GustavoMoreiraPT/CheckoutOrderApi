using Application.Dto;
using Application.Services.Implementations;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Domain.Core;
using Domain.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Tests
{

    [TestFixture]
    public class OrdersServiceTests
    {
        private IFixture fixture;
        private IUnitOfWork unitOfWorkMock;

        private OrdersService targetService;

        [SetUp]
        public void Setup()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            this.unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this.targetService = new OrdersService(unitOfWorkMock);
        }

        [Test]
        public void Get_Returns_List_WithSucess()
        {
            //Arrange
            var customerId = Guid.NewGuid();

            var customer = fixture.Build<Customer>().With(x => x.Id, customerId).Create();
            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);

            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = customerId,
                    OrderItems = new List<OrderItem>{}
                },
                new Order
                {
                    CustomerId = customerId,
                    OrderItems = new List<OrderItem>{}
                }
            };

            this.unitOfWorkMock.OrderRepository.GetAll().Returns(orders);

            //Act
            var result = this.targetService.GetAll(customerId);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }

        [Test]
        public void Get_ThrowsException_When_Customer_Does_Not_Exist()
        {
            //Arrange
            var customerId = Guid.NewGuid();

            Customer customer = null;
            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);

            //Act
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => this.targetService.GetAll(customerId));
        }

        [Test]
        public void GetById_Returns_Order_With_Success()
        {
            //Arrange
            var customerId = Guid.NewGuid();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                OrderItems = new List<OrderItem>()
            };

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act
            var result = this.targetService.GetById(order.Id, customerId);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(order.Id);
        }

        [Test]
        public void GetById_ThrowsException_When_Order_Does_Not_Exist()
        {
            //Arrange
            var customerId = Guid.NewGuid();

            Order order = null;

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
                (() => this.targetService.GetById(Guid.NewGuid(), customerId));
        }

        [Test]
        public void GetById_ThrowsException_When_Customer_Does_Not_Belong_To_Order()
        {
            //Arrange
            var customerId = Guid.NewGuid();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                OrderItems = new List<OrderItem>()
            };

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act
            NUnit.Framework.Assert.Throws<ArgumentException>
                (() => this.targetService.GetById(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Test]
        public void Create_Order_With_Success()
        {
            //Arrange
            var orderDto = fixture.Build<OrderDto>().Create();
            var customer = fixture.Build<Customer>().Create();

            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);

            //Act
            this.targetService.Create(orderDto);

            //Assert
            this.unitOfWorkMock.OrderRepository.Received(1).Create(Arg.Any<Order>());
            this.unitOfWorkMock.Received(1).Save();
        }

        [Test]
        public void Create_Order_ThrowsException_When_Dto_Is_Null()
        {
            //Arrange
            OrderDto order = null;

            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Create(order));

            this.unitOfWorkMock.OrderRepository.Received(0).Create(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }

        [Test]
        public void Create_Order_ThrowsException_When_Customer_Does_Not_Exist()
        {
            //Arrange
            var orderDto = fixture.Build<OrderDto>().Create();
            Customer customer = null;

            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);

            //Act
            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Create(orderDto));

            //Assert
            this.unitOfWorkMock.OrderRepository.Received(0).Create(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }

        [Test]
        public void Delete_Order_With_Success()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            var order = new Order
            {
                Id = orderId,
                CustomerId = customerId,
                OrderItems = new List<OrderItem> { }
            };

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act
            this.targetService.Delete(orderId, customerId);

            //Assert
            this.unitOfWorkMock.OrderRepository.Received(1).Delete(Arg.Any<Order>());
        }

        [Test]
        public void Delete_Order_ThrowsException_When_Order_Does_Not_Exist()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            Order order = null;

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Delete(orderId, customerId));

            this.unitOfWorkMock.OrderRepository.Received(0).Delete(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }

        [Test]
        public void Delete_Order_ThrowsException_When_Customer_Does_Not_Belong_To_Order()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            Order order = new Order
            {
                Id = orderId,
                CustomerId = Guid.NewGuid()
            };

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Delete(orderId, customerId));

            this.unitOfWorkMock.OrderRepository.Received(0).Delete(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }

        [Test]
        public void Update_Order_With_Success()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            var order = new Order
            {
                Id = orderId,
                CustomerId = customerId
            };

            var orderDto = new OrderDto
            {
                Id = orderId,
                CustomerId = customerId,
                OrderItems = new List<OrderItemDto> { }
            };

            var customer = new Customer
            {
                Id = customerId
            };

            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);
            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act
            this.targetService.Update(orderDto);

            //Assert
            this.unitOfWorkMock.OrderRepository.Received(1).Edit(Arg.Any<Order>());
            this.unitOfWorkMock.Received(1).Save();
        }

        [Test]
        public void Update_Order_ThrowsException_When_Customer_Does_NotExist()
        {
            //Arrange
            var orderDto = new OrderDto
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderItems = new List<OrderItemDto> { }
            };

            Customer customer = null;

            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(new Order { });

            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Update(orderDto));

            this.unitOfWorkMock.OrderRepository.Received(0).Edit(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }

        [Test]
        public void Update_Order_ThrowsException_When_Dto_Is_Null()
        {
            //Arrange
            OrderDto orderDto = null;

            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(new Order { });

            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Update(orderDto));

            this.unitOfWorkMock.OrderRepository.Received(0).Edit(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }

        [Test]
        public void Update_Order_ThrowsException_When_OrderDto_Does_Not_Correspond_ExistingOrder()
        {
            //Arrange
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            Order order = null;

            var orderDto = new OrderDto
            {
                Id = orderId,
                CustomerId = customerId,
                OrderItems = new List<OrderItemDto> { }
            };

            var customer = new Customer
            {
                Id = customerId
            };

            this.unitOfWorkMock.CustomerRepository.GetById(Arg.Any<Guid>()).Returns(customer);
            this.unitOfWorkMock.OrderRepository.GetById(Arg.Any<Guid>()).Returns(order);

            //Act
            //Act & Assert
            NUnit.Framework.Assert.Throws<ArgumentNullException>
                (() => this.targetService.Update(orderDto));

            //Assert
            this.unitOfWorkMock.OrderRepository.Received(0).Edit(Arg.Any<Order>());
            this.unitOfWorkMock.Received(0).Save();
        }
    }
}
