using CensoRegional.Domain.Dtos;
using System.Collections.Generic;

namespace CensoRegional.Domain.Commands
{
    public class PersonCreatedCommand : BaseCommand { 
        public PersonCreateDto Person { get; set; }
        public IEnumerable<PersonCreatedCommand> Parents { get; set; }
        public IEnumerable<PersonCreatedCommand> Children { get; set; }
    }
}
