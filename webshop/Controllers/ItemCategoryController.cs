using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _itemCategoryService;

        public ItemCategoryController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategory>>> GetAll()
        {
            var itemCategories = await _itemCategoryService.GetAllAsync();
            return Ok(itemCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCategory>> GetById(int id)
        {
            var itemCategory = await _itemCategoryService.GetByIdAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return Ok(itemCategory);
        }

        [HttpPost]
        public async Task<ActionResult<ItemCategory>> Add(ItemCategory itemCategory)
        {
            await _itemCategoryService.AddAsync(itemCategory);
            return CreatedAtAction(nameof(GetById), new { id = itemCategory.IDItemCategory }, itemCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ItemCategory itemCategory)
        {
            if (id != itemCategory.IDItemCategory)
            {
                return BadRequest();
            }

            await _itemCategoryService.UpdateAsync(itemCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemCategoryService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<ItemCategory>> GetByName(string name)
        {
            var itemCategory = await _itemCategoryService.GetByNameAsync(name);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return Ok(itemCategory);
        }
    }
}
