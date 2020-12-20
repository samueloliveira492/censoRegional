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
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonDeleteCommandHandler(IBusPublisher busPublisher, IPersonRepository personRepository, IMapper mapper)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PersonDeleteCommand request, CancellationToken cancellationToken)
        {
            Person person = _mapper.Map<Person>(request.Person);
            await _personRepository.DeletePerson(person);
            await _busPublisher.PublishAsync(new PersonDeleteEvent { Name = request.Person.Name, LastName = request.Person.LastName });
            return Unit.Value;
        }
    }
}
