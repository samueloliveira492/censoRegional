using System.Threading.Tasks;

namespace CensoRegional.Domain.Messaging
{
    public interface IBusCommandPublisher
    {
        Task PublishCommandAsync<TCommand>(TCommand @command) where TCommand : ICommand;
    }
}
