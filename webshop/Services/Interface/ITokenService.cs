using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interface
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserDTO user);
    }
}
