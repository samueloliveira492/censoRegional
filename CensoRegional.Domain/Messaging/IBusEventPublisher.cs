using MediatR;
using System.Threading.Tasks;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusEventPublisher
    {
        Task PublishEventAsync<TNotification>(TNotification @event) where TNotification : INotification;
    }
}
