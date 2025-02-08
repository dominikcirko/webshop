using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface IStatusService : IGenericService<StatusDTO>
    {
        Task<StatusDTO> GetByNameAsync(string name);

    }
}
