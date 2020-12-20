using System.Threading.Tasks;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
