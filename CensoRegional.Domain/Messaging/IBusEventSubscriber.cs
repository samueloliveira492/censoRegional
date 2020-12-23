using MediatR;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusEventSubscriber
    {
        IBusEventSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest;
    }
}
