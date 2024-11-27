using ApplicationDTO.MSSQL.Users;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.PaginationHelpers;
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
                throw new Exception("User was not found");

            var response = _mapper.Map<UserDTO>(user);

            return response;
        }

        public async Task<PagedList<UserDTO>> GetAll(UserParams userParams)
        {
            //If I need to query additionaly before data retrieval
            IQueryable<User> query = _context.Users;

            //var users = await _context.Users.ToListAsync();

            //if (users == null)
            //    throw new Exception("No users found");

            //var response = users.Select(p => _mapper.Map<UserDTO>(p)).ToList();

            var totalCount = await _context.Users.CountAsync();
            var users = await query.ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .Skip((userParams.PageNumber - 1) * userParams.PageSize)
                .Take(userParams.PageSize)
                .ToListAsync();

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
