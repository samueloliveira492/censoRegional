using AutoMapper;
using CensoRegional.Domain.Commands;
using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Events;
using CensoRegional.Domain.Messaging;
using CensoRegional.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Application.CommandHandlers
{
    class PersonDeleteCommandHandler : IRequestHandler<PersonDeleteCommand>
    {
        private readonly IBusEventPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;
        public PersonDeleteCommandHandler(IBusEventPublisher busPublisher, IPersonRepository personRepository)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(PersonDeleteCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.DeletePerson(request.Name, request.LastName);
            await _busPublisher.PublishEventAsync(new PersonDeleteEvent { Name = request.Name, LastName = request.LastName });
            return Unit.Value;
        }
    }
}
