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
using Dawit.Infrastructure.Repositories.ef;
using Dawit.Infrastructure.Service.Auth;
using Dawit.Infrastructure.Service.Neural;
using Dawit.API.Service.Extensions;
using Dawit.Infrastructure.Service.Signal;
using Dawit.API.Service.Signal;
using Dawit.API.Hubs;

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

            services.AddDistributedMemoryCache(); //TODO: change for redis cache

            services.AddDbContext<BaseContext>(options => options.UseNpgsql(Configuration.GetConnectionString("gdb")));

            services.AddScoped(AuthFactory.JWTTokenService);
            services.AddTokenAuthentication(Configuration["Jwt:secret"]);

            services.AddScoped<INeuralNetworkRepository, NeuralNetworkRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INeuralNetworkService, NeuralNetworkService>();

            services.AddSingleton<IMsgContext<IModel>, RabbitContext>();
            services.AddSingleton<IMsgProducer, RabbitProducer>();
            services.AddSingleton<IMsgConsumer, RabbitConsumer>();
            services.AddSingleton<IConnectionMapping<Guid>, MemoryCacheMapping>();

            services.AddHostedService<NeuralReturnConsumer>();

            //TODO: add redis backplate
            services.AddSignalR();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BaseContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dawit.API v1"));

                /*app.UseCors(c => c.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod().AllowAnyHeader());*/
            	app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true).AllowCredentials());
            	Console.WriteLine("-----------EM DEV---------");
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            dbContext.Database.EnsureCreated();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notifications", opts =>
                {
                    opts.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                });

            });

        }
    }
}
