using System.Threading.Tasks;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusEventPublisher
    {
        Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
