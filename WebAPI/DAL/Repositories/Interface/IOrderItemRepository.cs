using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetByItemIdAsync(int itemId);
    }
}
