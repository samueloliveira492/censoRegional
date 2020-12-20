namespace CensoRegional.Domain.Events
{
    public class PersonCreateEvent : BaseEvent
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
