using CensoRegional.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Application.EventHandlers
{
    public class PersonCreatedEventHandler : IRequestHandler<PersonCreateEvent>
    {
        public async Task<Unit> Handle(PersonCreateEvent request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
