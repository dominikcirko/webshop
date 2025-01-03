using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface ITagService : IGenericService<TagDTO>
    {
        Task<TagDTO> GetByNameAsync(string name);
        Task<IEnumerable<TagDTO>> GetTagsByItemIdAsync(int itemId);
    }
}
