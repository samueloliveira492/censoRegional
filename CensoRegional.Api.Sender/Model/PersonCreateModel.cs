using CensoRegional.Domain.Commands;
using CensoRegional.Domain.Dto;
using System.Collections.Generic;

namespace CensoRegional.Api.Sender.Models
{
    public class PersonCreateModel
    {
        public PersonCreateDto Person { get; set; }
        public IEnumerable<PersonCreateModel> Parents { get; set; }
        public IEnumerable<PersonCreateModel> Children { get; set; }

        public PersonCreatedCommand ToPersonCreatedCommand(PersonCreateModel model)
        {
            PersonCreatedCommand personCreatedCommand = new PersonCreatedCommand();
            personCreatedCommand.Person = new PersonCreateDto();
            personCreatedCommand.Person.Name = model.Person.Name;
            personCreatedCommand.Person.LastName = model.Person.LastName;
            personCreatedCommand.Person.LevelEducation = model.Person.LevelEducation;
            personCreatedCommand.Person.Color = model.Person.Color;

            return personCreatedCommand;
        }
    }

    
}

