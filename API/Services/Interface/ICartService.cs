using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface ICartService : IGenericService<CartDTO>
    {
        Task<CartDTO> GetByUserIdAsync(int userId);
    }
}
