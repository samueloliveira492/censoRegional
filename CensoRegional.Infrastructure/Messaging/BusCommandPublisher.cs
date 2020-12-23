using CensoRegional.Domain.Messaging;
using RawRabbit;
using RawRabbit.Configuration.Exchange;
using System;
using System.Threading.Tasks;

namespace CensoRegional.Infrastructure.Messaging
{
    public class BusCommandPublisher : IBusCommandPublisher
    {
        private readonly IBusClient _busClient;

        public BusCommandPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishCommandAsync<TCommand>(TCommand @command) where TCommand : ICommand
        {
            await _busClient.PublishAsync(@command, ctx => ctx
            .UsePublishConfiguration(cfg => cfg
                .OnDeclaredExchange(e => e
                    .WithName("censoregional.domain.commands")
                    .WithType(ExchangeType.Direct))
                .WithRoutingKey(typeof(TCommand).Name)));
        }
            
    }
}
