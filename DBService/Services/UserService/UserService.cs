using ApplicationDTO.MSSQL.Users;
using AutoMapper;
using DBModels;
using DBModels.Enums;
using DBService.Data;
using Microsoft.EntityFrameworkCore;

namespace DBService.Services.UserService
{
    public class UserService(DataContext _context, IMapper _mapper) : IUserService
    {
        public async Task<UserDTO> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
                throw new Exception("User was not found");

            var response = _mapper.Map<UserDTO>(user);

            return response;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
                throw new Exception("No users found");

            var response = users.Select(p => _mapper.Map<UserDTO>(p)).ToList();

            return response;
        }

        public async Task<bool> Add(AddUserDTO userDto)
        {
            User user = _mapper.Map<AddUserDTO, User>(userDto);

            _context.Users.Add(user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            return isSaved;
        }

        public async Task<bool> Update(UpdateUserDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null)
                throw new Exception($"Person does not exist");

            _mapper.Map(userDto, user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            return isSaved;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new Exception($"Person does not exist. Or was already deleted");

            _context.Users.Remove(user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            return isSaved;
        }
    }
}
