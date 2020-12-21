using CensoRegional.Domain.Messaging;
using RawRabbit;
using RawRabbit.Configuration.Exchange;
using System;
using System.Threading.Tasks;

namespace CensoRegional.Infrastructure.Messaging
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            try
            {
                await _busClient.PublishAsync(@event, ctx => ctx
                .UsePublishConfiguration(cfg => cfg
                    .OnDeclaredExchange(e => e
                        .WithName("censoregional.domain.events"))
                    .WithRoutingKey(typeof(TEvent).Name)));
            } catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
            
    }
}
