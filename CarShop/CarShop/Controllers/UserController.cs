using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CarShop.Common.Models.User;

namespace CarShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        private readonly ICartService _cartService;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger,
            ICartService cartService)
        {
            _userService = userService;
            _logger = logger;
            _cartService = cartService;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!User.IsInRole(UserRoles.Admin.ToString()))
            {
                return BadRequest();
            }

            var result = await _userService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            var user = await _userService
                .GetUserByIdAsync(userId);

            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPut]
        public async Task UpdateUser([FromBody] User user)
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!(user is null)
                && user.Id.ToString().Equals(userIdString)
                || User.IsInRole(UserRoles.Admin.ToString()))
            {
                await _userService.UpdateAsync(user);
            }
        }

        public async Task<IActionResult> GetCartForUser()
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            var user = await _userService
                .GetUserByIdAsync(userId);

            var cart = await _cartService
                .GetCartByIdAsync(user.CartId);
            if (cart is null)
            {
                return BadRequest();
            }

            return Ok(cart);
        }

        public async Task<IActionResult> AddCarForCart(int carId)
        {
            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            return Ok(await _cartService.AddNewCarInCartAsync(carId, userId));
        }


    }
}
