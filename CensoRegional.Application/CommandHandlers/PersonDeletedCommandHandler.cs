﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CensoRegional.Application.CommandHandlers
{
    class PersonDeletedCommandHandler : IRequestHandler<PersonCreatedCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonCreatedCommandHandler(IBusPublisher busPublisher, IPersonRepository personRepository, IMapper mapper)
        {
            _busPublisher = busPublisher;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PersonCreatedCommand request, CancellationToken cancellationToken)
        {
            Person mainPerson = _mapper.Map<Person>(request.Person);
            await _personRepository.CreatePerson(mainPerson);
            CreatePersonAndRelationship(request.Parents, mainPerson, true);
            CreatePersonAndRelationship(request.Children, mainPerson, false);
            await _busPublisher.PublishAsync(new PersonCreatedEvent { Name = request.Person.Name, LastName = request.Person.LastName });
            return Unit.Value;
        }
    }