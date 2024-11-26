using ApplicationDTO.MSSQL.Users;
using AutoMapper;
using DBModels;
using DBService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DBService.Services.AccountService
{
    public class AccountService(DataContext _context, IMapper _mapper) : IAccountService
    {
        public async Task<bool> Register(RegisterUserDTO registerUser)
        {
            if (await IsExistingUser(registerUser.Name))
                throw new Exception("User already exists");

            var user = _mapper.Map<User>(registerUser);

            CreatePasswordHash(registerUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            var isSaved = await _context.SaveChangesAsync() > 0;

            return isSaved;
        }

        public async Task<string> Login(LoginUserDTO login, string tokenKey)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name!.ToLower() == login.Username.ToLower());

            if (user == null) throw new Exception("Invalid username");

            if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Invalid password");
            }
            else
            {
                return GenerateToken(user, tokenKey);
            }

        }

        private async Task<bool> IsExistingUser(string username)
        {
            return await _context.Users.AnyAsync(u => u.Name.ToLower() == username.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string GenerateToken(User user, string tokenKey)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
