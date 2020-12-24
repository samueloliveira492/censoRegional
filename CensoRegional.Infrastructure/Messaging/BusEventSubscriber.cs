using CensoRegional.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using RawRabbit;
using Serilog;
using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Configuration.Exchange;

namespace CensoRegional.Infrastructure.Messaging
{
    public class BusEventSubscriber : IBusEventSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;

        public BusEventSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public IBusEventSubscriber SubscribeEvent<TNotification>() where TNotification : MediatR.INotification
        {
            _busClient.SubscribeAsync<TNotification>(async (@notification) =>
            {
                try
                {
                    var scope = _serviceProvider.CreateScope();
                    var handler = scope.ServiceProvider.GetService<IMediator>();
                    await handler.Send(@notification);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro ao processar mensagem");
                    throw;
                }

            }, ctx => ctx.UseSubscribeConfiguration(cfg => cfg
                .Consume(c => c.WithRoutingKey(typeof(TNotification).Name))
                .FromDeclaredQueue(q => q
                    .WithName(GetQueueName<TNotification>())
                    .WithDurability()
                    .WithAutoDelete(false))
                .OnDeclaredExchange(e => e
                  .WithName("censoregional.domain.events")
                  .WithType(ExchangeType.Topic)
                  .WithArgument("key", typeof(TNotification).Name.ToLower()))
            ));
            return this;
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
    }
}
