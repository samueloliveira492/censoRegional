﻿using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Commands;
using CensoRegional.Domain.Events;
using CensoRegional.Domain.Messaging;
using CensoRegional.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CensoRegional.Util.Constantes;
using AutoMapper;
using System.Linq;

namespace CensoRegional.Application.CommandHandlers
{
    public class PersonCreateCommandHandler : IRequestHandler<PersonCreateCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonCreateCommandHandler(IBusPublisher busPublisher, IPersonRepository personRepository, IMapper mapper)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PersonCreateCommand request, CancellationToken cancellationToken)
        {
            Person mainPerson = _mapper.Map<Person>(request.Person);
            await _personRepository.CreatePerson(mainPerson);
            CreatePersonAndRelationship(request.Parents, mainPerson, true);
            CreatePersonAndRelationship(request.Children, mainPerson, false);
            await _busPublisher.PublishAsync(new PersonCreateEvent { Name = request.Person.Name, LastName = request.Person.LastName });
           
            return Unit.Value;
        }

        private void CreatePersonAndRelationship(IEnumerable<PersonCreateCommand> list, Person mainPerson, bool isParent)
        {
            foreach (var p in list)
            {
                if (p != null)
                {
                    Person rel = _mapper.Map<Person>(p.Person);
                    if (rel.IsValid())
                    {
                        Person cleaning = _personRepository.GetByNameAndLastName(rel.Name, rel.LastName).Result.FirstOrDefault();
                        if (cleaning == null)
                            _personRepository.CreatePerson(rel);

                        if (isParent)
                        {
                            _personRepository.CreateRelationship(rel, mainPerson, Relacionamentos.PARENT);
                        }
                        else
                        {
                            _personRepository.CreateRelationship(mainPerson, rel, Relacionamentos.PARENT);
                        }

                        if (p.Parents != null && p.Parents.Any())
                            CreatePersonAndRelationship(p.Parents, rel, true);
                        if (p.Children != null &&  p.Children.Any())
                            CreatePersonAndRelationship(p.Children, rel, false);
                    }
                }
                
            }
        }
    }
}