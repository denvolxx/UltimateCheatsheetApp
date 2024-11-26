using ApplicationDTO.MSSQL.Users;
using ApplicationDTO.MSSQL.Users.Enums;
using DBModels;
using DBService.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using UltimateCheatsheetApp.Controllers.Base;

namespace UltimateCheatsheetApp.Controllers
{
    public class UserController(IUserService _userService) : BaseApiController
    {
        [HttpGet("{userId}")]
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
