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

        /// <summary>
        /// Gets all the available orders for a specific customer.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer. Used as a query parameter.</param>
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

        /// <summary>
        /// Retrieve of a specific detailed order for a specific customer.
        /// </summary>
        /// <param name="id">The unique identifier of the order. Used as path of the request.</param>
        /// <param name="customerId">The unique identifier of the customer. Used as a query parameter.</param>
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

        /// <summary>
        /// Gets all different items from an order.
        /// </summary>
        /// <param name="id">The unique identifier of the order. Used as path of the request.</param>
        /// <param name="customerId">The unique identifier of the customer. Used as a query parameter.</param>
        [HttpGet]
        [Route("{id}/items")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrderItems([FromRoute] Guid id, [FromQuery] Guid customerId)
        {
            if (id == Guid.Empty || customerId == Guid.Empty)
            {
                return this.BadRequest();
            }

            var items = this.ordersService.GetOrderItems(id, customerId);

            return this.Ok(items);
        }

        /// <summary>
        /// Update an existing order for a specific customer.
        /// </summary>
        /// <param name="id">The unique identifier of the order to update. Used as path of the request.</param>
        /// <param name="order">The body of the order to be updated. Should contain the entire object to be updated.</param>
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

        /// <summary>
        /// Deletes an existing order for a specific customer.
        /// </summary>
        /// <param name="id">The unique identifier of the order to delete. Used as path of the request.</param>
        /// <param name="customerId">The unique identifier of the customer. Used as a query parameter.</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid id, [FromQuery] Guid customerId)
        {
            if (id == Guid.Empty || customerId == Guid.Empty)
            {
                return this.BadRequest();
            }

            this.ordersService.Delete(id, customerId);

            return this.NoContent();
        }

        /// <summary>
        /// Creates a specific detailed order for a specific customer.
        /// </summary>
        /// <param name="order">The body of the order to update.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody]OrderDto order)
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
                order.Id = Guid.NewGuid();
            }

            this.ordersService.Create(order);

            return this.Created($"https://localhost:44392/v1/orders/{order.Id}", order.Id);
        }
    }
}
