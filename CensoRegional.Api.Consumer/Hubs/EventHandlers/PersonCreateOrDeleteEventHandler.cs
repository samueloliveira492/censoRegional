using CensoRegional.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Api.Consumer.Hubs.EventHandlers
{
    public class PersonCreateOrDeleteEventHandler : IRequestHandler<PersonCreateOrDeleteEvent>
    {
        private readonly IHubContext<PersonEventHub> _hubContext;
        public PersonCreateOrDeleteEventHandler(IHubContext<PersonEventHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public async Task<Unit> Handle(PersonCreateOrDeleteEvent @notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("personCommandExecuted", @notification, cancellationToken);
            return Unit.Value;
        }
    }
}
