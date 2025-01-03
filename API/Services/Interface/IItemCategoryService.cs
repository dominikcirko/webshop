using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface IItemCategoryService : IGenericService<ItemCategoryDTO>
    {
        Task<ItemCategoryDTO> GetByNameAsync(string categoryName);
    }
}
