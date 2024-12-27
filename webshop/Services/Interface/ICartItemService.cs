using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.DTOs;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface ICartItemService : IGenericService<CartItemDTO>
    {
        Task<IEnumerable<CartItemDTO>> GetByCartIdAsync(int cartId);
    }
}
