using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface ITagService : IGenericService<Tag>
    {
        Task<Tag> GetByNameAsync(string name);
        Task<IEnumerable<Tag>> GetTagsByItemIdAsync(int itemId);
    }
}
