using MediatR;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusEventSubscriber
    {
        IBusEventSubscriber SubscribeEvent<TNotification>() where TNotification : INotification;
    }
}
