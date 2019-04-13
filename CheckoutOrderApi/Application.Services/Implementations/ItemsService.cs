using System.Collections.Generic;
using Application.Dto;
using Application.Services.Interfaces;
using Application.Services.Mapping;
using Domain.Core;

namespace Application.Services.Implementations
{
    public class ItemsService : IItemsService
    {
        private readonly IUnitOfWork unitOfWork;

        public ItemsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.unitOfWork.SeedItemsData();
        }

        public List<ItemDto> GetAll()
        {
            var items = this.unitOfWork.ItemRepository.GetAll();

            return items.ToDtoList();
        }
    }
}
