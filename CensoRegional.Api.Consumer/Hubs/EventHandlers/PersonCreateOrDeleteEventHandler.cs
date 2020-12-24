using CensoRegional.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Api.Consumer.Hubs.EventHandlers
{
    public class PersonCreateOrDeleteEventHandler : INotificationHandler<PersonCreateOrDeleteEvent>
    {
        private readonly IHubContext<PersonEventHub> _hubContext;
        public PersonCreateOrDeleteEventHandler(IHubContext<PersonEventHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public Task Handle(PersonCreateOrDeleteEvent @event, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("personCommandExecuted", @event, cancellationToken);
        }
    }
}
