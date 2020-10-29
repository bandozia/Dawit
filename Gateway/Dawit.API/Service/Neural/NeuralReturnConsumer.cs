using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Service.Messaging;
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
        
        public NeuralReturnConsumer(IMsgConsumer msgConsumer, IServiceProvider services)
        {
            _msgConsumer = msgConsumer;
            _services = services;            
            
            RegisterDefaultConsumers();
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
        }

        private async Task OnTrainComplete(JobResult result)
        {
            using (var scope = _services.CreateScope())
            {
                var neuralRepo = scope.ServiceProvider.GetRequiredService<INeuralNetworkRepository>();
                //TODO: update neural job
            }
            //TODO: notify subscribed clients
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
