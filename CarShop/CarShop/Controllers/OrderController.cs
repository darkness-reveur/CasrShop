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
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly IUserService _userService;

        private readonly ILogger _logger;

        public OrderController(
            IOrderService orderService,
            ILogger<OrderController> logger,
            IUserService userService)
        {
            _logger = logger;
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUserOrders")]
        [Authorize]
        public async Task<ActionResult> GerAllUserOrders()
        {

            var userIdString = User.FindFirst("userid")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest();
            }

            var orders = await _orderService.GerAllUserOrdersAsync(userId);


            if (orders is null)
            {
                return BadRequest();
            }

            return Ok(orders);
        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            var order = await _orderService
                .GetOrderByIdAsync(Id);

            if(order is null)
            {
                return BadRequest();
            }
            return Ok(order);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] int cartId)
        {
            var order = await _orderService
                .CreateOrderAsync(cartId);

            if (order is null)
            {
                return BadRequest();
            }

            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> ApproveOrder([FromBody] int orderId)
        {
            var updateOrder = await _orderService
                .ApproveOrderAsync(orderId);

            if (updateOrder is null)
            {
                return BadRequest();
            }

            return Ok(updateOrder);
        }

        [HttpDelete]
        [Route("delete/{orderid}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (!User.IsInRole(UserRoles.Admin.ToString()))
            {
                return BadRequest();
            }

            var isDeleted = await _orderService
                .DeleteOrderDyIdAsync(orderId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(isDeleted);
        }
    }
}
