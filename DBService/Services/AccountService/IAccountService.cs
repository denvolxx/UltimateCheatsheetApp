using ApplicationDTO.MSSQL.Users;

namespace DBService.Services.AccountService
{
    public interface IAccountService
    {
        Task<bool> Register(RegisterUserDTO registerUser);
        Task<string> Login(LoginUserDTO login, string tokenKey);
    }
}
