﻿using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Models;
using webshopAPI.DTOs;

namespace webshopAPI.Services.Interfaces
{
    public interface IUserService : IGenericService<UserDTO>
    {
        Task<UserDTO> GetByUsernameAsync(string username);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAdminsAsync();
    }
}
