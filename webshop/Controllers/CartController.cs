using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAll()
        {
            var carts = await _cartService.GetAllAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetById(int id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> Add(Cart cart)
        {
            await _cartService.AddAsync(cart);
            return CreatedAtAction(nameof(GetById), new { id = cart.IDCart }, cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cart cart)
        {
            if (id != cart.IDCart)
            {
                return BadRequest();
            }

            await _cartService.UpdateAsync(cart);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<Cart>> GetByUserId(int userId)
        {
            var cart = await _cartService.GetByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
    }
}
