using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IItemCategoryRepository _itemCategoryRepository;

        public ItemCategoryService(IItemCategoryRepository itemCategoryRepository)
        {
            _itemCategoryRepository = itemCategoryRepository;
        }

        public async Task<IEnumerable<ItemCategory>> GetAllAsync()
        {
            return await _itemCategoryRepository.GetAllAsync();
        }

        public async Task<ItemCategory> GetByIdAsync(int id)
        {
            return await _itemCategoryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ItemCategory itemCategory)
        {
            await _itemCategoryRepository.AddAsync(itemCategory);
        }

        public async Task UpdateAsync(ItemCategory itemCategory)
        {
            await _itemCategoryRepository.UpdateAsync(itemCategory);
        }

        public async Task DeleteAsync(int id)
        {
            await _itemCategoryRepository.DeleteAsync(id);
        }

        public async Task<ItemCategory> GetByNameAsync(string categoryName)
        {
            return await _itemCategoryRepository.GetByNameAsync(categoryName);
        }

        public async Task<bool> CategoryExistsAsync(string categoryName)
        {
            return await _itemCategoryRepository.CategoryExistsAsync(categoryName);
        }
    }
}
