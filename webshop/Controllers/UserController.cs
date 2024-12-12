using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Services.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add(User user)
        {
            await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.IDUser }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.IDUser)
            {
                return BadRequest();
            }

            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByUsername/{username}")]
        public async Task<ActionResult<User>> GetByUsername(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<User>> Authenticate(string username, string password)
        {
            var user = await _userService.AuthenticateAsync(username, password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpGet("Admins")]
        public async Task<ActionResult<IEnumerable<User>>> GetAdmins()
        {
            var admins = await _userService.GetAdminsAsync();
            return Ok(admins);
        }
    }
}
