using ApplicationDTO.MSSQL.Users;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Helpers;
using Common.QueryParameters;
using DBModels;
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
                throw new Exception("User does not exist");

            var response = _mapper.Map<UserDTO>(user);

            return response;
        }

        public async Task<PagedList<UserDTO>> GetAll(UserParams userParams)
        {
            //If I need to query additionaly before data retrieval
            IQueryable<User> query = _context.Users;

            var totalCount = await _context.Users.CountAsync();
            var users = await query.ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .Skip((userParams.PageNumber - 1) * userParams.PageSize)
                .Take(userParams.PageSize)
                .ToListAsync();

            if (users == null)
                throw new Exception("Can not retrieve users");

            return new PagedList<UserDTO>(users, userParams.PageSize, userParams.PageNumber, totalCount);
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
                throw new Exception($"User does not exist");

            _mapper.Map(userDto, user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            return isSaved;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new Exception($"User does not exist. Or was already deleted");

            _context.Users.Remove(user);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            return isSaved;
        }
    }
}
