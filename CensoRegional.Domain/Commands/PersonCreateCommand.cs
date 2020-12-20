using CensoRegional.Domain.Dtos;
using System.Collections.Generic;

namespace CensoRegional.Domain.Commands
{
    public class PersonCreateCommand : BaseCommand { 
        public PersonCreateDto Person { get; set; }
        public IEnumerable<PersonCreateCommand> Parents { get; set; }
        public IEnumerable<PersonCreateCommand> Children { get; set; }
    }
}
