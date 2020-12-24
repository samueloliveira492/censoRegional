using CensoRegional.Domain.Messaging;
using MediatR;
using RawRabbit;
using RawRabbit.Configuration.Exchange;
using System;
using System.Threading.Tasks;

namespace CensoRegional.Infrastructure.Messaging
{
    public class BusEventPublisher : IBusEventPublisher
    {
        private readonly IBusClient _busClient;

        public BusEventPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishEventAsync<TNotification>(TNotification @event) where TNotification : INotification
        {
            await _busClient.PublishAsync(@event, ctx => ctx
            .UsePublishConfiguration(cfg => cfg
                .OnDeclaredExchange(e => e
                    .WithName("censoregional.domain.events")
                    .WithType(ExchangeType.Topic))
                .WithRoutingKey(typeof(TNotification).Name)));
        }
            
    }
}
