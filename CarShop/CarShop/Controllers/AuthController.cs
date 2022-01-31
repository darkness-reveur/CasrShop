﻿using CarShop.Auth;
using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Common.Models.AuthenticationModels;
using CarShop.Common.Models.Enums;
using CarShop.Infrastructure.Services.AuthenticationService;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IUserService _userService;

        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            IUserService userService,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("IsLoginFree/{login}")]
        public async Task<IActionResult> IsLoginFree(string login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            return Ok(await _authService
                .IsLoginFree(login));
        }

        [HttpPut]
        public async Task<IActionResult> LogInAsync([FromBody] RegisterData data)
        {
            if (data.Login == null
                && data.Password == null)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthController),
                    nameof(LogInAsync),
                    $"Got an error. Login={data.Login} | Password={data.Password}",
                    new ArgumentNullException());

                return Ok(false);
            }

            var user = await _authService.LogIn(
                    data.Login,
                    data.Password);

            if (user == null)
            {
                _logger.LogWarning("Login data was incorrect");

                return Ok(false);
            }

            var now = DateTime.UtcNow;

           var identity = Authenticate(
                user.Id.ToString(),
                user.Role.ToString(),
                data.Login);

            return Ok();
        }

        private  ClaimsIdentity Authenticate(string userId, string userRole, string userLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(
                     ClaimsIdentity.DefaultNameClaimType,
                     userLogin),

                new Claim(
                    ClaimsIdentity.DefaultRoleClaimType,
                    userRole),

                 new Claim(
                    "userid",
                    userId),
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            //await HttpContext.SignInAsync(
            //   JwtBearerDefaults.AuthenticationScheme,
            //   new ClaimsPrincipal(identity));

            return identity;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterData data)
        {
            if (data.Login == null
                || data.Password == null
                || data.User == null)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthController),
                    nameof(RegisterAsync),
                    $"Got an error. Login={!(data.Login is null)} " +
                        $"| Password={!(data.Password is null)} " +
                        $"| User={!(data.User is null)}",
                    new ArgumentNullException());

                return Ok(false);
            }

            var user = await _userService
                .AddAsync(data.User);

            await _authService.Register(
                data.Login,
                data.Password,
                user);

            return Ok(true);
        }

        //[HttpGet]
        //[Route("LogOut")]
        //public async Task<IActionResult> LogoutAsync()
        //{
        //    //Врядли работает на момент 09 11 2021
        //    try
        //    {
        //        await HttpContext.SignOutAsync(
        //             JwtBearerDefaults.AuthenticationScheme);

        //        return Ok(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorByTemplate(
        //            nameof(AuthController),
        //            nameof(LogoutAsync),
        //            $"LogOut error",
        //            ex);
        //        return BadRequest();
        //    }
        //}
    }
}
