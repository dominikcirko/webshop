using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, int statusId);
    }
}
