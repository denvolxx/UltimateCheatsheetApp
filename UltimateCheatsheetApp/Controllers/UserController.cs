using ApplicationDTO.MSSQL.Users;
using DBModels;
using DBService.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using UltimateCheatsheetApp.Controllers.Base;

namespace UltimateCheatsheetApp.Controllers
{
    public class UserController(IUserService _userService) : BaseApiController
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUser(int userId)
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
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
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
        public async Task<ActionResult> AddUser(AddUserDTO user)
        {
            var response = await _userService.Add(user);

            if (response)
            {
                return Ok($"User {user.Name} was created");
            }
            else
            {
                return BadRequest("Unable to create user");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO user)
        {
            var response = await _userService.Update(user);
            if (response)
            {
                return Ok($"User {user.Name} was updated");
            }
            else
            {
                return BadRequest("Unable to update user");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _userService.Delete(id);

            if (response)
            {
                return Ok($"User was deleted");
            }
            else
            {
                return BadRequest("Unable to delete user");
            }
        }
    }
}
