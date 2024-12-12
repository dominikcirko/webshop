using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _orderItemRepository.GetAllAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _orderItemRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _orderItemRepository.AddAsync(orderItem);
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            await _orderItemRepository.UpdateAsync(orderItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _orderItemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _orderItemRepository.GetByOrderIdAsync(orderId);
        }

        public async Task<IEnumerable<OrderItem>> GetByItemIdAsync(int itemId)
        {
            return await _orderItemRepository.GetByItemIdAsync(itemId);
        }
    }
}
