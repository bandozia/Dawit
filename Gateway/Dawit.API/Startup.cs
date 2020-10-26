using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Dawit.Infrastructure.Service.Messaging;
using Dawit.Infrastructure.Service.Messaging.Rabbit;
using RabbitMQ.Client;
using Dawit.API.Service.Neural;
using Dawit.Infrastructure.Repositories;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Repositories.ef;

namespace Dawit.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
                
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dawit.API", Version = "v1" });
            });

            services.AddDbContext<BaseContext>(options => options.UseNpgsql(Configuration.GetConnectionString("gdb")));

            services.AddScoped<INeuralJobRepository, NeuralJobRepository>();
            services.AddTransient<NeuralJobService>();
                        
            services.AddSingleton<IMsgContext<IModel>, RabbitContext>();            
            services.AddSingleton<IMsgProducer, RabbitProducer>();
            services.AddSingleton<IMsgConsumer, RabbitConsumer>();
            services.AddHostedService<NeuralReturnConsumer>();
            
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BaseContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dawit.API v1"));                                
            }

            app.UseRouting();
            app.UseAuthorization();

            dbContext.Database.EnsureCreated();
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
