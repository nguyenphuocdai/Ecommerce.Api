﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetwood.API.Bus;
using Streetwood.API.Filters;
using Streetwood.Core.Extensions;
using Streetwood.Infrastructure.Commands.Models.Order;
using Streetwood.Infrastructure.Dto;
using Streetwood.Infrastructure.Queries.Models.Order;

namespace Streetwood.API.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IBus bus;

        public OrdersController(IBus bus)
        {
            this.bus = bus;
        }

        // admin can see all, but customer only his own
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
            => Ok(await bus.SendAsync(new GetOrderQueryModel(id)));

        [HttpGet]
        [IgnoreValidation]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetFilteredOrdersQueryModel model)
            => Ok(await bus.SendAsync(model.SetUserId(User.GetUserId()).SetUserType(User.GetUserType())));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AddOrderCommandModel model)
        {
            var orderId = await bus.SendAsync(model.SetUserId(User.GetUserId()));
            return Ok(new NewOrderDto(orderId));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateOrderCommandModel model)
        {
            await bus.SendAsync(model.SetId(id));
            return Accepted();
        }
    }
}