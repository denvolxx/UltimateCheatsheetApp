using ApplicationDTO.MSSQL.Users;
using DBModels;

namespace DBService.Services.UserService
{
    public interface IUserService
    {
        Task<UserDTO> GetById(int id);
        Task<List<UserDTO>> GetAll();
        Task<bool> Add(AddUserDTO user);
        Task<bool> Update(UpdateUserDTO user);
        Task<bool> Delete(int id);
    }
}
