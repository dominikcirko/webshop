using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag> GetByNameAsync(string name);
        Task<IEnumerable<Tag>> GetTagsByItemIdAsync(int itemId);
    }
}
