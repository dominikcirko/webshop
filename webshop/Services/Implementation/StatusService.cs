using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _statusRepository.GetAllAsync();
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            return await _statusRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Status status)
        {
            await _statusRepository.AddAsync(status);
        }

        public async Task UpdateAsync(Status status)
        {
            await _statusRepository.UpdateAsync(status);
        }

        public async Task DeleteAsync(int id)
        {
            await _statusRepository.DeleteAsync(id);
        }

        public async Task<Status> GetByNameAsync(string name)
        {
            return await _statusRepository.GetByNameAsync(name);
        }
    }
}
