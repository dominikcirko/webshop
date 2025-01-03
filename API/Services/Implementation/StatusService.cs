using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.DTOs;
using AutoMapper;
using webshopAPI.Services.Interface;

namespace webshopAPI.Services.Implementation
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, ILogService logService, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusDTO>> GetAllAsync()
        {
            var statuses = await _statusRepository.GetAllAsync();
            _logService.LogAction("Info", "Retrieved all statuses.");
            return _mapper.Map<IEnumerable<StatusDTO>>(statuses);
        }

        public async Task<StatusDTO> GetByIdAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status == null)
            {
                _logService.LogAction("Warning", $"Status with id={id} not found.");
                return null;
            }

            _logService.LogAction("Info", $"Retrieved status with id={id}.");
            return _mapper.Map<StatusDTO>(status);
        }

        public async Task AddAsync(StatusDTO statusDto)
        {
            var status = _mapper.Map<Status>(statusDto);
            await _statusRepository.AddAsync(status);
            _logService.LogAction("Info", $"Status with id={status.IDStatus} has been added.");
        }

        public async Task UpdateAsync(StatusDTO statusDto)
        {
            var status = _mapper.Map<Status>(statusDto);
            await _statusRepository.UpdateAsync(status);
            _logService.LogAction("Info", $"Status with id={status.IDStatus} has been updated.");
        }

        public async Task DeleteAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status == null)
            {
                _logService.LogAction("Warning", $"Attempted to delete non-existent status with id={id}.");
                return;
            }

            await _statusRepository.DeleteAsync(id);
            _logService.LogAction("Info", $"Status with id={id} has been deleted.");
        }

        public async Task<StatusDTO> GetByNameAsync(string name)
        {
            var status = await _statusRepository.GetByNameAsync(name);

            if (status == null)
            {
                _logService.LogAction("Warning", $"Status with name '{name}' not found.");
                return null;
            }

            _logService.LogAction("Info", $"Retrieved status with name '{name}'.");
            return _mapper.Map<StatusDTO>(status);
        }
    }
}