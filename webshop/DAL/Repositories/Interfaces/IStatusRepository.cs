using webshopAPI.Models;

namespace webshopAPI.DataAccess.Repositories.Interfaces
{
    public interface IStatusRepository : IRepository<Status>
    {
        Task<Status> GetByNameAsync(string name);
    }
}
