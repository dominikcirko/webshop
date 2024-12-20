﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            var items = await _itemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            var item = await _itemService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Add(Item item)
        {
            await _itemService.AddAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = item.IDItem }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            if (id != item.IDItem)
            {
                return BadRequest();
            }

            await _itemService.UpdateAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetByCategoryId(int categoryId)
        {
            var items = await _itemService.GetByCategoryIdAsync(categoryId);
            return Ok(items);
        }

        [HttpGet("ByTag/{tagId?}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetByTagId(int? tagId)
        {
            var items = await _itemService.GetByTagIdAsync(tagId);
            return Ok(items);
        }

        [HttpGet("SearchByTitle/{title}")]
        public async Task<ActionResult<IEnumerable<Item>>> SearchByTitle(string title)
        {
            var items = await _itemService.SearchByTitleAsync(title);
            return Ok(items);
        }

        [HttpGet("IsInStock/{itemId}")]
        public async Task<ActionResult<bool>> IsInStock(int itemId)
        {
            var inStock = await _itemService.IsInStockAsync(itemId);
            return Ok(inStock);
        }
    }
}
