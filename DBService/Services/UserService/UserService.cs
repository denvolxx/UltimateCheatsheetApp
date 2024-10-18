using ApplicationDTO.Users;
using DBService.Data;
using DBService.Models;
using DBService.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DBService.Services.UserService
{
    public class UserService(DataContext _context) : IUserService
    {
        public async Task<User> GetById(int id)
        {
            User? response = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (response == null)
            {
                throw new Exception("Unable to retrieve user");
            }

            return response;
        }

        public async Task<List<User>> GetAll()
        {
            List<User> response = await _context.Users.ToListAsync();

            if (response == null)
            {
                throw new Exception("Unable to retrieve users");
            }

            return response;
        }

        public async Task<AddUserDTO> AddUser(AddUserDTO userDto)
        {
            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Gender = GenderEnum.Male
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return userDto;
        }


    }
}
