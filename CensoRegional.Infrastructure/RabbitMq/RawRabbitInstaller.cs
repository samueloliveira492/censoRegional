﻿using CensoRegional.Domain.Messaging;
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
                Plugins = p => p
                    .UseGlobalExecutionId()
                    .UseHttpContext()
                    .UseMessageContext(c => new MessageContext { GlobalRequestId = Guid.NewGuid() })
            });
            services.AddSingleton<IBusClient>(_ => client);

            return services;
        }

        public static IBusEventSubscriber UseRabbitMq(this IApplicationBuilder app) => new BusEventSubscriber(app);
    }
}
