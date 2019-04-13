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

        }

        [Test]
        public void GetById_ThrowsException_When_Order_Does_Not_Exist()
        {

        }

        [Test]
        public void GetById_ThrowsException_When_Customer_Does_Not_Belong_To_Order()
        {

        }

        [Test]
        public void Create_Order_With_Success()
        {
            
        }

        [Test]
        public void Create_Order_ThrowsException_When_Dto_Is_Null()
        {

        }

        [Test]
        public void Create_Order_ThrowsException_When_Customer_Does_Not_Exist()
        {

        }

        [Test]
        public void Delete_Order_With_Success()
        {

        }

        [Test]
        public void Delete_Order_ThrowsException_When_Order_Does_Not_Exist()
        {

        }

        [Test]
        public void Delete_Order_ThrowsException_When_Customer_Does_Not_Belong_To_Order()
        {

        }

        [Test]
        public void Update_Order_With_Success()
        {
            
        }

        [Test]
        public void Update_Order_ThrowsException_When_Dto_Is_Null()
        {

        }

        [Test]
        public void Update_Order_ThrowsException_When_Customer_Does_NotExist()
        {

        }

        [Test]
        public void Update_Order_ThrowsException_When_OrderDto_Does_Not_Correspond_ExistingOrder()
        {

        }
    }
}
