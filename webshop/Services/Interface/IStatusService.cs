using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface IStatusService : IGenericService<Status>
    {
        Task<Status> GetByNameAsync(string name);

    }
}
