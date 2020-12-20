using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Commands;
using CensoRegional.Domain.Events;
using CensoRegional.Domain.Messaging;
using CensoRegional.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CensoRegional.Util.Constantes;

namespace CensoRegional.Application.CommandHandlers
{
    public class PersonCreatedCommandHandler : IRequestHandler<PersonCreatedCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;
        public PersonCreatedCommandHandler(IBusPublisher busPublisher, IPersonRepository personRepository)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(PersonCreatedCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.CreatePerson(request.Person);
            CreatePersonAndRelationship(request.Parents, request.Person, true);
            CreatePersonAndRelationship(request.Children, request.Person, false);
            await _busPublisher.PublishAsync(new PersonCreatedEvent {  Name = request.Person.Name, LastName = request.Person.LastName });
            return Unit.Value;
        }

        private void CreatePersonAndRelationship(IEnumerable<Person> list, Person mainPerson, bool isParent)
        {
            foreach (var p in list)
            {
                if (p.IsValid())
                {
                    _personRepository.CreatePerson(p);
                    if (isParent)
                    {
                        _personRepository.CreateRelationship(p, mainPerson, Relacionamentos.PARENT);
                    }
                    else
                    {
                        _personRepository.CreateRelationship(mainPerson, p, Relacionamentos.PARENT);
                    }   
                }
            }
        }
    }
}
