﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Streetwood.Infrastructure.Commands.Models;
using Streetwood.Infrastructure.Commands.Models.Charm;
using Streetwood.Infrastructure.Services.Abstract.Commands;

namespace Streetwood.Infrastructure.Commands.Handlers.Charm
{
    public class AddCharmCommandHandler : IRequestHandler<AddCharmCommandModel, Guid>
    {
        private readonly ICharmCommandService charmCommandService;

        public AddCharmCommandHandler(ICharmCommandService charmCommandService)
        {
            this.charmCommandService = charmCommandService;
        }

        public async Task<Guid> Handle(AddCharmCommandModel request, CancellationToken cancellationToken)
             => await charmCommandService.AddAsync(request.Name, request.NameEng, request.Price, request.CharmCategoryId);
    }
}
