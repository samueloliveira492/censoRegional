namespace CensoRegional.Domain.Events
{
    public class PersonCreateOrDeleteEvent : BaseEvent
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
    }
}
