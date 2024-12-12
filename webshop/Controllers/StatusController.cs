using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetAll()
        {
            var statuses = await _statusService.GetAllAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetById(int id)
        {
            var status = await _statusService.GetByIdAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<Status>> Add(Status status)
        {
            await _statusService.AddAsync(status);
            return CreatedAtAction(nameof(GetById), new { id = status.IDStatus }, status);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Status status)
        {
            if (id != status.IDStatus)
            {
                return BadRequest();
            }

            await _statusService.UpdateAsync(status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _statusService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<Status>> GetByName(string name)
        {
            var status = await _statusService.GetByNameAsync(name);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }
    }
}
