using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface IItemCategoryRepository : IGenericRepository<ItemCategory>
    {
        Task<ItemCategory> GetByNameAsync(string categoryName);
        Task<bool> CategoryExistsAsync(string categoryName);
    }
}
