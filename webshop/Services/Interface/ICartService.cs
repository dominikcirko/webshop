using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface ICartService : IGenericService<Cart>
    {
        Task<Cart> GetByUserIdAsync(int userId);
    }
}
