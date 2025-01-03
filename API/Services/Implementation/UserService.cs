using AutoMapper;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.DTOs;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using webshopAPI.Services.Interface;

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
        var user = _mapper.Map<User>(userDto);
        await _userRepository.UpdateAsync(user);
        _logService.LogAction("Info", $"User with id={user.IDUser} has been updated.");
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