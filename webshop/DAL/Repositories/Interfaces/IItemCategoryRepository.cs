using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface IItemCategoryRepository : IRepository<ItemCategory>
    {
        Task<ItemCategory> GetByNameAsync(string categoryName);
        Task<bool> CategoryExistsAsync(string categoryName);
    }
}
