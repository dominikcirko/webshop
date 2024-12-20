﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetAll()
        {
            var cartItems = await _cartItemService.GetAllAsync();
            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetById(int id)
        {
            var cartItem = await _cartItemService.GetByIdAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return Ok(cartItem);
        }

        [HttpPost]
        public async Task<ActionResult<CartItem>> Add(CartItem cartItem)
        {
            await _cartItemService.AddAsync(cartItem);
            return CreatedAtAction(nameof(GetById), new { id = cartItem.IDCartItem }, cartItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CartItem cartItem)
        {
            if (id != cartItem.IDCartItem)
            {
                return BadRequest();
            }

            await _cartItemService.UpdateAsync(cartItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartItemService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByCart/{cartId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetByCartId(int cartId)
        {
            var cartItems = await _cartItemService.GetByCartIdAsync(cartId);
            return Ok(cartItems);
        }
    }
}
