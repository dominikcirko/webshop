using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAll()
        {
            var tags = await _tagService.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetById(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> Add(Tag tag)
        {
            await _tagService.AddAsync(tag);
            return CreatedAtAction(nameof(GetById), new { id = tag.IDTag }, tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tag tag)
        {
            if (id != tag.IDTag)
            {
                return BadRequest();
            }

            await _tagService.UpdateAsync(tag);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<Tag>> GetByName(string name)
        {
            var tag = await _tagService.GetByNameAsync(name);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpGet("ByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsByItemId(int itemId)
        {
            var tags = await _tagService.GetTagsByItemIdAsync(itemId);
            return Ok(tags);
        }
    }
}
