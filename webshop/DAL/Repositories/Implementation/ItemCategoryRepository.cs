using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly webshopdbContext _context;

        public ItemCategoryRepository(webshopdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemCategory>> GetAllAsync()
        {
            return await _context.ItemCategories.ToListAsync();
        }

        public async Task<ItemCategory> GetByIdAsync(int id)
        {
            return await _context.ItemCategories.FindAsync(id);
        }

        public async Task AddAsync(ItemCategory entity)
        {
            await _context.ItemCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemCategory entity)
        {
            _context.ItemCategories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.ItemCategories.FindAsync(id);
            if (category != null)
            {
                _context.ItemCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ItemCategory> GetByNameAsync(string categoryName)
        {
            return await _context.ItemCategories
                .FirstOrDefaultAsync(ic => ic.CategoryName == categoryName);
        }

        public async Task<bool> CategoryExistsAsync(string categoryName)
        {
            return await _context.ItemCategories
                .AnyAsync(ic => ic.CategoryName == categoryName);
        }
    }
}
