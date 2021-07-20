using CarShop.Common.Helpers;
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
        public ActionResult<bool> IsLoginFree(string login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            return _authService
                .IsLoginFree(login);
        }

        [HttpPut]
        public ActionResult<bool> LogIn([FromBody] AccessDataEntity data)
        {
            if (data.Login == null
                && data.Password == null)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthController),
                    nameof(LogIn),
                    $"Got an error. Login={data.Login} | Password={data.Password}",
                    new ArgumentNullException());

                return Ok(false);
            }

            var user = _authService.LogIn(
                    data.Login,
                    data.Password);

            if (user == null)
            {
                _logger.LogWarning("Login data was incorrect");

                return false;
            }

             Authenticate(
                user.Id.ToString(),
                user.Role);

            _logger.LogInfo(
                nameof(AuthController),
                nameof(LogIn),
                $"User {user.Id} has been authenticated");

            return Ok(true);
        }

        private void Authenticate(
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

             HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        [HttpPost]
        public ActionResult<bool> Register([FromBody] RegisterData data)
        {
            if (data.Login == null
                || data.Password == null
                || data.User == null)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthController),
                    nameof(Register),
                    $"Got an error. Login={!(data.Login is null)} " +
                        $"| Password={!(data.Password is null)} " +
                        $"| User={!(data.User is null)}",
                    new ArgumentNullException());

                return Ok(false);
            }

            try
            {
                var user = _userService
                    .Add(data.User);

                _authService.Register(
                    data.Login,
                    data.Password,
                    user);

                Authenticate(
                    user.Id.ToString(),
                    user.Role);

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthController),
                    nameof(Register),
                    $"Authentication error",
                    ex);

                return false;
            }
        }
    }
}
