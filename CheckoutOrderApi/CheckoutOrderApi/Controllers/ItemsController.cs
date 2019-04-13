using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CheckoutOrderApi.Controllers
{
    [Route("v1/items")]
    public class ItemsController : Controller
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        /// <summary>
        /// Gets all the available products to be used in orders.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllItems()
        {
            var items = this.itemsService.GetAll();

            return this.Ok(items);
        }
    }
}
