using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Common.Models.AuthenticationModels;
using CarShop.Infrastructure.Services.AuthenticationService;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static CarShop.Common.Models.User;

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
        public async Task<IActionResult> LogInAsync([FromBody] AccessDataEntity data)
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

             await Authenticate(
                user.Id.ToString(),
                user.Role);

            _logger.LogInfo(
                nameof(AuthController),
                nameof(LogInAsync),
                $"User {user.Id} has been authenticated");

            return Ok(true);
        }

        private async Task Authenticate(
            string userId,
            UserRoles userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(
                    "userid",
                    userId),

                new Claim(
                    ClaimsIdentity.DefaultRoleClaimType,
                    userRole.ToString())
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

             await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
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

            try
            {
                var user = await _userService
                    .AddAsync(data.User);

                await _authService.Register(
                    data.Login,
                    data.Password,
                    user);

                await Authenticate(
                    user.Id.ToString(),
                    user.Role);

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthController),
                    nameof(RegisterAsync),
                    $"Authentication error",
                    ex);

                return BadRequest();
            }
        }
    }
}
