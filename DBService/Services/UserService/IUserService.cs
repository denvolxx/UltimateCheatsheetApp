using ApplicationDTO.MSSQL.Users;
using Common.Helpers;
using Common.QueryParameters;

namespace DBService.Services.UserService
{
    public interface IUserService
    {
        Task<UserDTO> GetById(int id);
        Task<PagedList<UserDTO>> GetAll(UserParams userParams);
        Task<bool> Add(AddUserDTO user);
        Task<bool> Update(UpdateUserDTO user);
        Task<bool> Delete(int id);
    }
}
