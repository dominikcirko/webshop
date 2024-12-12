using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;

namespace webshopAPI.Services.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> AuthenticateAsync(string username, string password);
        Task<IEnumerable<User>> GetAdminsAsync();
    }
}
