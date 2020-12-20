namespace CensoRegional.Domain.Events
{
    public class PersonCreatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
