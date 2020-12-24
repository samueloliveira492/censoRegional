namespace CensoRegional.Domain.Events
{
    public class PersonCreateOrDeleteEvent : BaseNotification
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
    }
}
