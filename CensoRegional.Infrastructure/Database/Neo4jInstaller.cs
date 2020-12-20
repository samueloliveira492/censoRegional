using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4jClient;
using System;

namespace CensoRegional.Infrastructure.Database
{
    public static class Neo4JInstaller
    {
        public static IServiceCollection AddDatabaseNeo4J(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("Neo4jConnectionSettings");
            var neo4jClient = new GraphClient(new Uri(config.GetSection("Server").Value), 
                config.GetSection("User").Value, config.GetSection("Pass").Value);

            neo4jClient.ConnectAsync().Wait();
            services.AddSingleton<IGraphClient>(neo4jClient);

            return services;
        }
    }
}
