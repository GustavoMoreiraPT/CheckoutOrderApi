using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CheckoutOrderApi.Controllers
{
    [Route("v1/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrders([FromQuery] Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return this.BadRequest();
            }

            var orders = this.ordersService.GetAll(customerId);

            return this.Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] Guid id, [FromQuery] Guid customerId)
        {
            if (id == Guid.Empty || customerId == Guid.Empty)
            {
                return this.BadRequest();
            }

            var order = this.ordersService.GetById(id, customerId);

            return this.Ok(order);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromRoute] Guid id, [FromBody]OrderDto order)
        {
            if (order == null)
            {
                return this.BadRequest();
            }

            if (order.CustomerId == Guid.Empty)
            {
                return this.BadRequest();
            }

            if (order.Id == Guid.Empty || order.Id != id)
            {
                return this.BadRequest();
            }

            this.ordersService.Update(order);

            return this.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid id, [FromQuery] Guid customerId)
        {
       
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatE([FromBody]OrderDto order)
        {
            if (order == null)
            {
                return this.BadRequest();
            }

            if (order.CustomerId == Guid.Empty)
            {
                return this.BadRequest();
            }

            if (order.Id == Guid.Empty)
            {
                order.Id = new Guid();
            }

            this.ordersService.Create(order);

            return this.Created(string.Empty, order.Id);
        }
    }
}
