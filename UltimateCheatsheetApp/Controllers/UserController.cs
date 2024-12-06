using ApplicationDTO.MSSQL.Users;
using Common.QueryParameters;
using DBService.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltimateCheatsheetApp.Controllers.Base;
using UltimateCheatsheetApp.Extensions;

namespace UltimateCheatsheetApp.Controllers
{
    [Authorize]
    public class UserController(IUserService _userService) : BaseApiController
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUser(int userId)
        {
            var response = await _userService.GetById(userId);

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers([FromQuery] UserParams userParams)
        {
            var users = await _userService.GetAll(userParams);

            if (users == null)
            {
                return NotFound();
            }
            else
            {
                //Add "Pagination" header with retrieval information. Extension method
                Response.AddPaginationHeader<UserDTO>(users);

                return Ok(users);
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
                return Ok($"User was updated");
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
