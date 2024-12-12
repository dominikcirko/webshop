using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface IItemCategoryService : IGenericService<ItemCategory>
    {
        Task<ItemCategory> GetByNameAsync(string categoryName);
    }
}
