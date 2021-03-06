﻿using CensoRegional.Infrastructure.Database;
using CensoRegional.Infrastructure.RabbitMq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using CensoRegional.Application.QueryHandlers;
using CensoRegional.Ioc;
using curso.Ioc;
using AutoMapper;
using CensoRegional.Domain.Events;
using CensoRegional.Api.Consumer.Hubs;
using CensoRegional.Api.Consumer.Hubs.EventHandlers;

namespace CensoRegional.Api.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddResolverDependencies();

            services.AddMediatR(typeof(PersonCreateOrDeleteEventHandler));
            services.AddMediatR(typeof(FamilyTreeByPersonQueryHandler));
            services.AddMediatR(typeof(PercentagePeopleSameNameByRegionQueryHandler));
            services.AddMediatR(typeof(QuantityPeopleByManyFiltersQueryHandler));
            services.AddAutoMapper(AssemblyReflection.GetCurrentAssemblies());
            services.AddRabbitMq(Configuration);
            services.AddDatabaseNeo4J(Configuration);
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
               .WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
               
            }));
            services.AddSignalR();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("consumer", new Info
                {
                    Version = "v1",
                    Title = "Censo Regional - Queries and Events",
                    Description = "",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Samuel de Oliveira",
                        Email = "samueloliveira492@gmail.com",
                        Url = ""
                    }

                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRabbitMqForEvent().SubscribeEvent<PersonCreateOrDeleteEvent>();

            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<PersonEventHub>("/person-events");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/consumer/swagger.json", "API consumer");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
