using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class StatusRepository : IStatusRepository
    {
        private readonly webshopdbContext _context;

        public StatusRepository(webshopdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            return await _context.Statuses.FindAsync(id);
        }

        public async Task AddAsync(Status entity)
        {
            await _context.Statuses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Status entity)
        {
            _context.Statuses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Status> GetByNameAsync(string name)
        {
            return await _context.Statuses
                .FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
