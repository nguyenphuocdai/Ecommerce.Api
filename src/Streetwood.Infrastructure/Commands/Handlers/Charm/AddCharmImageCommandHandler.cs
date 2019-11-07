using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Streetwood.Infrastructure.Commands.Models.Charm;
using Streetwood.Infrastructure.Services.Abstract.Commands;

namespace Streetwood.Infrastructure.Commands.Handlers.Charm
{
    public class AddCharmImageCommandHandler : IRequestHandler<AddCharmImageCommandModel, Unit>
    {
        private readonly ICharmCommandService charmCommandService;

        public AddCharmImageCommandHandler(ICharmCommandService charmCommandService)
        {
            this.charmCommandService = charmCommandService;
        }

        public async Task<Unit> Handle(AddCharmImageCommandModel request, CancellationToken cancellationToken)
        {
            await charmCommandService.AddPhotoAsync(request.Id, request.File);
            return Unit.Value;
        }
    }
}
