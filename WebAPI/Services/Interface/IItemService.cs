using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface IItemService : IGenericService<ItemDTO>
    {
        Task<IEnumerable<ItemDTO>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<ItemDTO>> SearchByTitleAsync(string title);
        Task<int> IsInStockAsync(int itemId);
    }
}
