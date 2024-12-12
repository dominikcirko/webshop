using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface ICartItemService : IGenericService<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
    }
}
