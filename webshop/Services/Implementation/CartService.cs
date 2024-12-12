using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _cartRepository.GetAllAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Cart cart)
        {
            await _cartRepository.AddAsync(cart);
        }

        public async Task UpdateAsync(Cart cart)
        {
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task DeleteAsync(int id)
        {
            await _cartRepository.DeleteAsync(id);
        }

        public async Task<Cart> GetByUserIdAsync(int userId)
        {
            return await _cartRepository.GetByUserIdAsync(userId);
        }
    }
}
