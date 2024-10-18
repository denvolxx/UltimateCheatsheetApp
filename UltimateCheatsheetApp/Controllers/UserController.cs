using ApplicationDTO.Users;
using ApplicationDTO.Users.Enums;
using Azure;
using DBService.Models;
using DBService.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UltimateCheatsheetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var response = await _userService.GetById(userId);

            if (response == null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var response = await _userService.GetAll();

            if (response == null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUser(User user)
        {
            AddUserDTO userDto = new AddUserDTO()
            {
                Name = user.Name,
                Email = user.Email,
                Gender = GenderEnum.Male
            };

            var response = await _userService.AddUser(userDto);

            return Ok($"User {response.Name} was created");
        }
    }
}
