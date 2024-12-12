using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly webshopdbContext _context;

        public CartItemRepository(webshopdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetByIdAsync(int id)
        {
            return await _context.CartItems.FindAsync(id);
        }

        public async Task AddAsync(CartItem entity)
        {
            await _context.CartItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartItem entity)
        {
            _context.CartItems.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                .Where(ci => ci.CartID == cartId)
                .ToListAsync();
        }
    }
}
