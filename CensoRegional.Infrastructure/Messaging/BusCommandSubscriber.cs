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
    public class BusCommandSubscriber : IBusCommandSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;

        public BusCommandSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public IBusCommandSubscriber SubscribeCommand<TCommand>() where TCommand : ICommand, IRequest
        {
            _busClient.SubscribeAsync<TCommand>(async (@command) =>
            {
                try
                {
                    var scope = _serviceProvider.CreateScope();
                    var handler = scope.ServiceProvider.GetService<IMediator>();
                    await handler.Send(@command);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro ao processar mensagem");
                    throw;
                }

            }, ctx => ctx.UseSubscribeConfiguration(cfg => cfg
                .Consume(c => c.WithRoutingKey(typeof(TCommand).Name))
                .FromDeclaredQueue(q => q
                    .WithName(GetQueueName<TCommand>())
                    .WithDurability()
                    .WithAutoDelete(false))
                .OnDeclaredExchange(e => e
                  .WithName("censoregional.domain.commands")
                  .WithType(ExchangeType.Topic)
                  .WithArgument("key", typeof(TCommand).Name.ToLower()))
            ));
            return this;
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
    }
}
