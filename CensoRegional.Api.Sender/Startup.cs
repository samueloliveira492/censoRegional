﻿using CensoRegional.Application.CommandHandlers;
using CensoRegional.Infrastructure.Database;
using CensoRegional.Infrastructure.RabbitMq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using CensoRegional.Ioc;
using curso.Ioc;
using AutoMapper;
using CensoRegional.Domain.Commands;
using CensoRegional.Domain.Events;

namespace CensoRegional.Api.Sender
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
            services.AddAutoMapper(AssemblyReflection.GetCurrentAssemblies());
            services.AddResolverDependencies();
            services.AddMediatR(typeof(PersonCreateCommandHandler));
            services.AddMediatR(typeof(PersonDeleteCommandHandler));


            services.AddRabbitMq(Configuration);

            services.AddDatabaseNeo4J(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("sender", new Info
                {
                    Version = "v1",
                    Title = "Censo Regional - Commands",
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
            app.UseRabbitMqForCommand().SubscribeCommand<PersonCreateCommand>();
            app.UseRabbitMqForCommand().SubscribeCommand<PersonDeleteCommand>();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/sender/swagger.json", "API Sender");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
