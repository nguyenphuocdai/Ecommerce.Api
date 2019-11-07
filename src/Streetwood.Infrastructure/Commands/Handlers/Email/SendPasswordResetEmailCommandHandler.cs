﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Streetwood.Infrastructure.Commands.Models.Email;
using Streetwood.Infrastructure.Services.Abstract;
using Streetwood.Infrastructure.Services.Abstract.Queries;

namespace Streetwood.Infrastructure.Commands.Handlers.Email
{
    public class SendPasswordResetEmailCommandHandler : IRequestHandler<SendPasswordResetEmailCommandModel>
    {
        private readonly IEmailService emailService;
        private readonly IUserQueryService userQueryService;
        private readonly ILogger logger;

        public SendPasswordResetEmailCommandHandler(IEmailService emailService, IUserQueryService userQueryService, 
            ILogger<SendPasswordResetEmailCommandHandler> logger)
        {
            this.emailService = emailService;
            this.userQueryService = userQueryService;
            this.logger = logger;
        }

        public async Task<Unit> Handle(SendPasswordResetEmailCommandModel request, CancellationToken cancellationToken)
        {
            var user = await userQueryService.CreateChangePasswordTokenAsync(request.Email);

            await emailService.SendForgottenPasswordEmailAsync(user);
            logger.LogInformation("Successfully send password");
            return Unit.Value;
        }
    }
}
