namespace CensoRegional.Domain.Commands
{
    public class PersonDeleteCommand : BaseCommand
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
