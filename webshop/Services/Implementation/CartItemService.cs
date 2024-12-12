using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _cartItemRepository.GetAllAsync();
        }

        public async Task<CartItem> GetByIdAsync(int id)
        {
            return await _cartItemRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CartItem cartItem)
        {
            await _cartItemRepository.AddAsync(cartItem);
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            await _cartItemRepository.UpdateAsync(cartItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _cartItemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return await _cartItemRepository.GetByCartIdAsync(cartId);
        }
    }
}
