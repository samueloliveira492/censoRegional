using CensoRegional.Application.EventHandlers;
using CensoRegional.Infrastructure.Database;
using CensoRegional.Infrastructure.RabbitMq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using CensoRegional.Application.QueryHandlers;

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

            services.AddMediatR(typeof(PersonCreatedEventHandler));
            services.AddMediatR(typeof(PercentagePeopleSameNameByRegionQueryHandler));

            services.AddRabbitMq(Configuration);

            services.AddDatabaseNeo4J(Configuration);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Api de criação de Pessoas",
                    Description = "Test Description",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Ali Ben Chaabene",
                        Email = "alibenchaabene@gmail.com",
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Test Version 1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "API Test Version 2");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
