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
        
        public async Task<Unit> Handle(PersonCreateOrDeleteEvent @request, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("personCommandExecuted", @request, cancellationToken);
            return Unit.Value;
        }
    }
}
