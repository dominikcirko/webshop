using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface IItemService : IGenericService<Item>
    {
        Task<IEnumerable<Item>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Item>> GetByTagIdAsync(int? tagId);
        Task<IEnumerable<Item>> SearchByTitleAsync(string title);
        Task<bool> IsInStockAsync(int itemId);
    }
}
