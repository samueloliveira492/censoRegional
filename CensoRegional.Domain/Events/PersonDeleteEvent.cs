namespace CensoRegional.Domain.Events
{
    public class PersonDeleteEvent : BaseEvent
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
