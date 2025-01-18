using AutoMapper;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.DTOs;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Services.Interface;
using System.Security.Cryptography;
using System.Text;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogService _logService;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, ILogService logService, IMapper mapper)
    {
        _userRepository = userRepository;
        _logService = logService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        _logService.LogAction("Info", "Retrieved all users.");
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            _logService.LogAction("Warning", $"User with id={id} not found.");
            return null;
        }

        _logService.LogAction("Info", $"Retrieved user with id={id}.");
        return _mapper.Map<UserDTO>(user);
    }

    public async Task AddAsync(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddAsync(user);
        _logService.LogAction("Info", $"User with id={user.IDUser} has been added.");
    }

    public async Task UpdateAsync(UserDTO userDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(userDto.IDUser);
        if (existingUser == null)
        {
            throw new Exception($"User with ID {userDto.IDUser} not found.");
        }

        existingUser.Username = userDto.Username;
        existingUser.FirstName = userDto.FirstName;
        existingUser.LastName = userDto.LastName;
        existingUser.Email = userDto.Email;
        existingUser.PhoneNumber = userDto.PhoneNumber;

        if (!string.IsNullOrEmpty(userDto.Password))
        {
            using var hmac = new HMACSHA256();
            existingUser.PasswordSalt = hmac.Key;
            existingUser.Password = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)));
        }

        await _userRepository.UpdateAsync(existingUser);
        _logService.LogAction("Info", $"User with id={userDto.IDUser} has been updated.");
    }



    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            _logService.LogAction("Warning", $"Attempted to delete non-existent user with id={id}.");
            return;
        }

        await _userRepository.DeleteAsync(id);
        _logService.LogAction("Info", $"User with id={id} has been deleted.");
    }

    public async Task<UserDTO> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null)
        {
            _logService.LogAction("Warning", $"User with username '{username}' not found.");
            return null;
        }

        _logService.LogAction("Info", $"Retrieved user with username '{username}'.");
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            _logService.LogAction("Warning", $"User with email '{email}' not found.");
            return null;
        }

        _logService.LogAction("Info", $"Retrieved user with email '{email}'.");
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<IEnumerable<UserDTO>> GetAdminsAsync()
    {
        var admins = await _userRepository.GetAdminsAsync();
        _logService.LogAction("Info", "Retrieved all admins.");
        return _mapper.Map<IEnumerable<UserDTO>>(admins);
    }
}