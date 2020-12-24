using CensoRegional.Domain.Messaging;
using CensoRegional.Infrastructure.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Enrichers.MessageContext.Context;
using RawRabbit.Instantiation;
using RawRabbit.Serialization;
using System;

namespace CensoRegional.Infrastructure.RabbitMq
{
    public static class RawRabbitInstaller
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RawRabbitConfiguration();
            configuration.GetSection("RabbitMq").Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options,
            });
            services.AddSingleton<IBusClient>(_ => client);

            return services;
        }

        public static IBusEventSubscriber UseRabbitMqForEvent(this IApplicationBuilder app) => new BusEventSubscriber(app);
        public static IBusCommandSubscriber UseRabbitMqForCommand(this IApplicationBuilder app) => new BusCommandSubscriber(app);
    }
}
