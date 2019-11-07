﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Streetwood.API.Bus;
using Streetwood.Infrastructure.Commands.Models.Email;
using Streetwood.Infrastructure.Commands.Models.Password;

namespace Streetwood.API.Controllers
{
    [Route("api/passwords")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly IBus bus;

        public PasswordsController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
            => Ok(await bus.SendAsync(new SendPasswordResetEmailCommandModel(email)));

        [HttpPost]
        public async Task<IActionResult> Post(UpdatePasswordCommandModel model)
        {
            await bus.SendAsync(model);
            return Accepted();
        }
    }
}