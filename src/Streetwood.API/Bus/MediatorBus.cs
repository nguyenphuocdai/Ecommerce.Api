﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Streetwood.API.Bus
{
    public class MediatorBus : IBus
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public MediatorBus(IMediator mediator, ILogger<MediatorBus> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Invoked '{request.GetType().Name}.");
            return mediator.Send(request, cancellationToken);
        }
    }
}
