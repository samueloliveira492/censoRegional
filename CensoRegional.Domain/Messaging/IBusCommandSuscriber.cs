using MediatR;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusCommandSubscriber
    {
        IBusCommandSubscriber SubscribeCommand<TCommand>() where TCommand : ICommand, IRequest;
    }
}
