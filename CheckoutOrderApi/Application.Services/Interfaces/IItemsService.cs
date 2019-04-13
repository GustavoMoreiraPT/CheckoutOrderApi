using Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IItemsService
    {
        List<ItemDto> GetAll();
    }
}
