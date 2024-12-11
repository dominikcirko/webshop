using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByUserIdAsync(int userId); 
        Task<bool> IsCartEmptyAsync(int cartId);
    }
}
