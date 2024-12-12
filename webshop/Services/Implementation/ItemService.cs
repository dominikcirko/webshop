using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Item item)
        {
            await _itemRepository.AddAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            await _itemRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Item>> GetByCategoryIdAsync(int categoryId)
        {
            return await _itemRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Item>> GetByTagIdAsync(int? tagId)
        {
            return await _itemRepository.GetByTagIdAsync(tagId);
        }

        public async Task<IEnumerable<Item>> SearchByTitleAsync(string title)
        {
            return await _itemRepository.SearchByTitleAsync(title);
        }

        public async Task<bool> IsInStockAsync(int itemId)
        {
            return await _itemRepository.IsInStockAsync(itemId);
        }
    }
}
