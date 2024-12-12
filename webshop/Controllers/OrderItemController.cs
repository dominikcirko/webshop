using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
        {
            var orderItems = await _orderItemService.GetAllAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetById(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> Add(OrderItem orderItem)
        {
            await _orderItemService.AddAsync(orderItem);
            return CreatedAtAction(nameof(GetById), new { id = orderItem.IDOrderItem }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItem orderItem)
        {
            if (id != orderItem.IDOrderItem)
            {
                return BadRequest();
            }

            await _orderItemService.UpdateAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderItemService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByOrder/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        [HttpGet("ByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetByItemId(int itemId)
        {
            var orderItems = await _orderItemService.GetByIdAsync(itemId);
            return Ok(orderItems);
        }
    }
}
