using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
    }
}
