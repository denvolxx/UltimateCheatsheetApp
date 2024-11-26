﻿using ApplicationDTO.MSSQL.Users;
using DBService.Data;
using DBService.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UltimateCheatsheetApp.Controllers.Base;

namespace UltimateCheatsheetApp.Controllers
{
    public class AccountController(IAccountService _accountService, IConfiguration _config) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUser)
        {
            var isRegistered = await _accountService.Register(registerUser);

            if (isRegistered)
            {
                return Ok($"User {registerUser.Name} was registered successfully");
            }
            else
            {
                return BadRequest("Unable to register user");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserDTO login)
        {
            var tokenKey = _config["TokenKey"] ?? throw new Exception("Cannot access token key");

            //if (tokenKey.Length < 64) 
            //    throw new Exception("Your tokenkey should have at least 64 symbols");

            var token = await _accountService.Login(login, tokenKey);

            return Ok(token);
        }
    }
}
