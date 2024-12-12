using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface IStatusRepository : IGenericRepository<Status>
    {
        Task<Status> GetByNameAsync(string name);
    }
}
