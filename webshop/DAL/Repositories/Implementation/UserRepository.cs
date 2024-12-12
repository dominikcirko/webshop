using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly webshopdbContext _context;

        public UserRepository(webshopdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            if (user.Password != password)
                throw new UnauthorizedAccessException("Invalid password.");
            return user;
        }

        public async Task<IEnumerable<User>> GetAdminsAsync()
        {
            return await _context.Users
                .Where(u => u.IsAdmin == true)
                .ToListAsync();
        }
    }
}
