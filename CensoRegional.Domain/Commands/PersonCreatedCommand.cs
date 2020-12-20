using CensoRegional.Domain.Entity;
using System.Collections.Generic;

namespace CensoRegional.Domain.Commands
{
    public class PersonCreatedCommand : BaseCommand { 
        public Person Person { get; set; }
        public IEnumerable<Person> Parents { get; set; }
        public IEnumerable<Person> Children { get; set; }
    }
}
