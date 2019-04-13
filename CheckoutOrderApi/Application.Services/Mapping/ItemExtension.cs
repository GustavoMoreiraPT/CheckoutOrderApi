using Application.Dto;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Mapping
{
    public static class ItemExtension
    {
        public static ItemDto ToDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category
            };
        }

        public static List<ItemDto> ToDtoList(this List<Item> items)
        {
            return items.Select(x => x.ToDto()).ToList();
        }
    }
}
