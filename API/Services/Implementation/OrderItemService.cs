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
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, ILogService logService, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            _logService.LogAction("Info", "Retrieved all order items.");
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<OrderItemDTO> GetByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);

            if (orderItem == null)
            {
                _logService.LogAction("Warning", $"Order item with id={id} not found.");
                return null;
            }

            _logService.LogAction("Info", $"Retrieved order item with id={id}.");
            return _mapper.Map<OrderItemDTO>(orderItem);
        }

        public async Task AddAsync(OrderItemDTO orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            await _orderItemRepository.AddAsync(orderItem);
            _logService.LogAction("Info", $"Order item with id={orderItem.IDOrderItem} has been added.");
        }

        public async Task UpdateAsync(OrderItemDTO orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            await _orderItemRepository.UpdateAsync(orderItem);
            _logService.LogAction("Info", $"Order item with id={orderItem.IDOrderItem} has been updated.");
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);

            if (orderItem == null)
            {
                _logService.LogAction("Warning", $"Attempted to delete non-existent order item with id={id}.");
                return;
            }

            await _orderItemRepository.DeleteAsync(id);
            _logService.LogAction("Info", $"Order item with id={id} has been deleted.");
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(int orderId)
        {
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(orderId);
            _logService.LogAction("Info", $"Retrieved order items for order id={orderId}.");
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByItemIdAsync(int itemId)
        {
            var orderItems = await _orderItemRepository.GetByItemIdAsync(itemId);
            _logService.LogAction("Info", $"Retrieved order items for item id={itemId}.");
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }
    }
}