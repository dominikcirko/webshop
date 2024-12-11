using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly webshopdbContext _context;

        public CartRepository(webshopdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task AddAsync(Cart entity)
        {
            await _context.Carts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart entity)
        {
            _context.Carts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems) // Include items if you need them
                .FirstOrDefaultAsync(c => c.UserID == userId);
        }

        public async Task<bool> IsCartEmptyAsync(int cartId)
        {
            // Check if any CartItems exist for this cart
            return !await _context.CartItems.AnyAsync(ci => ci.CartID == cartId);
        }
    }
}
