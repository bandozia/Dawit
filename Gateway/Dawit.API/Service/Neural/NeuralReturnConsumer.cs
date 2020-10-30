using Dawit.API.Hubs;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Service.Messaging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dawit.API.Service.Neural
{
    public class NeuralReturnConsumer : IHostedService
    {
        private readonly IMsgConsumer _msgConsumer;
        private readonly IServiceProvider _services;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public NeuralReturnConsumer(IMsgConsumer msgConsumer, IServiceProvider services, IHubContext<NotificationHub> notificationHub)
        {
            _msgConsumer = msgConsumer;
            _services = services;

            RegisterDefaultConsumers();
            _notificationHub = notificationHub;
        }

        private void RegisterDefaultConsumers()
        {
            _msgConsumer.AddQueueToConsume<NeuralMetric>(Queues.NN_TRAIN_PROGRESS, false, OnTrainProgress);
            _msgConsumer.AddQueueToConsume<JobResult>(Queues.NN_TRAIN_COMPLETE, true, OnTrainComplete);
        }

        private async Task OnTrainProgress(NeuralMetric progress)
        {
            //TODO: add metric on db
            //TODO: notify subscribed clients
            await _notificationHub.Clients.All.SendAsync("onTrainProgress", progress);//test
        }

        private async Task OnTrainComplete(JobResult result)
        {
            using (var scope = _services.CreateScope())
            {
                var neuralRepo = scope.ServiceProvider.GetRequiredService<INeuralNetworkRepository>();
                //TODO: update neural job
            }
            //TODO: notify subscribed clients
            await _notificationHub.Clients.All.SendAsync("onTrainComplete", result.JobId);//test
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
