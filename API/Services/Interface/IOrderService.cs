using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface IOrderService : IGenericService<OrderDTO>
    {
        Task<IEnumerable<OrderDTO>> GetByUserIdAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, int statusId);
    }
}
