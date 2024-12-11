using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly webshopdbContext _context;

        public ItemRepository(webshopdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task AddAsync(Item entity)
        {
            await _context.Items.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Item entity)
        {
            _context.Items.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Item>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Items
                .Where(i => i.ItemCategoryID == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetByTagIdAsync(int? tagId)
        {
            if (tagId.HasValue)
            {
                return await _context.Items
                    .Where(i => i.TagID == tagId.Value)
                    .ToListAsync();
            }
            else
            {
                // If tagId is null, return items that have no tag set
                return await _context.Items
                    .Where(i => i.TagID == null)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Item>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Item>();

            var loweredTitle = title.ToLower();
            return await _context.Items
                .Where(i => i.Title.ToLower().Contains(loweredTitle))
                .ToListAsync();
        }

        public async Task<bool> IsInStockAsync(int itemId)
        {
            // Check if item is in stock
            var inStock = await _context.Items
                .Where(i => i.IDItem == itemId)
                .Select(i => i.InStock)
                .FirstOrDefaultAsync();

            return inStock;
        }
    }
}
