using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.DTOs;
using AutoMapper;
using webshopAPI.Services.Interface;

namespace webshopAPI.Services.Implementation
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IItemCategoryRepository _itemCategoryRepository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public ItemCategoryService(IItemCategoryRepository itemCategoryRepository, ILogService logService, IMapper mapper)
        {
            _itemCategoryRepository = itemCategoryRepository;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemCategoryDTO>> GetAllAsync()
        {
            var itemCategories = await _itemCategoryRepository.GetAllAsync();
            _logService.LogAction("Info", "Retrieved all item categories.");
            return _mapper.Map<IEnumerable<ItemCategoryDTO>>(itemCategories);
        }

        public async Task<ItemCategoryDTO> GetByIdAsync(int id)
        {
            var itemCategory = await _itemCategoryRepository.GetByIdAsync(id);

            if (itemCategory == null)
            {
                _logService.LogAction("Warning", $"Item category with id={id} not found.");
                return null;
            }

            _logService.LogAction("Info", $"Retrieved item category with id={id}.");
            return _mapper.Map<ItemCategoryDTO>(itemCategory);
        }

        public async Task AddAsync(ItemCategoryDTO itemCategoryDto)
        {
            var itemCategory = _mapper.Map<ItemCategory>(itemCategoryDto);
            await _itemCategoryRepository.AddAsync(itemCategory);
            _logService.LogAction("Info", $"Item category with id={itemCategory.IDItemCategory} has been added.");
        }

        public async Task UpdateAsync(ItemCategoryDTO itemCategoryDto)
        {
            var itemCategory = _mapper.Map<ItemCategory>(itemCategoryDto);
            await _itemCategoryRepository.UpdateAsync(itemCategory);
            _logService.LogAction("Info", $"Item category with id={itemCategory.IDItemCategory} has been updated.");
        }

        public async Task DeleteAsync(int id)
        {
            var itemCategory = await _itemCategoryRepository.GetByIdAsync(id);

            if (itemCategory == null)
            {
                _logService.LogAction("Warning", $"Attempted to delete non-existent item category with id={id}.");
                return;
            }

            await _itemCategoryRepository.DeleteAsync(id);
            _logService.LogAction("Info", $"Item category with id={id} has been deleted.");
        }

        public async Task<ItemCategoryDTO> GetByNameAsync(string categoryName)
        {
            var itemCategory = await _itemCategoryRepository.GetByNameAsync(categoryName);

            if (itemCategory == null)
            {
                _logService.LogAction("Warning", $"Item category with name '{categoryName}' not found.");
                return null;
            }

            _logService.LogAction("Info", $"Retrieved item category with name '{categoryName}'.");
            return _mapper.Map<ItemCategoryDTO>(itemCategory);
        }

        public async Task<bool> CategoryExistsAsync(string categoryName)
        {
            var exists = await _itemCategoryRepository.CategoryExistsAsync(categoryName);

            if (exists)
            {
                _logService.LogAction("Info", $"Item category with name '{categoryName}' exists.");
            }
            else
            {
                _logService.LogAction("Info", $"Item category with name '{categoryName}' does not exist.");
            }

            return exists;
        }
    }
}