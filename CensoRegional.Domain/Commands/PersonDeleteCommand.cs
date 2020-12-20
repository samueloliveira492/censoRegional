using CensoRegional.Domain.Dtos;

namespace CensoRegional.Domain.Commands
{
    public class PersonDeleteCommand : BaseCommand
    {
        public PersonCreateDto Person { get; set; }
    }
}
