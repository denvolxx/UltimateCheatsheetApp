using ApplicationDTO.MSSQL.Users;
using DBModels;

namespace DBService.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task<AddUserDTO> AddUser(AddUserDTO user);
    }
}
