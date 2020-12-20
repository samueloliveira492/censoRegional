using CensoRegional.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Application.EventHandlers
{
    public class PersonCreatedEventHandler
    {
        public async Task<Unit> Handle(PersonCreatedEvent request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
