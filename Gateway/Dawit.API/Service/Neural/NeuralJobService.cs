using Dawit.API.Model.Form;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Service.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.Service.Neural
{
    public class NeuralJobService
    {
        private readonly INeuralJobRepository _neuralJobRepository;
        private readonly IMsgProducer _eventProducer;

        public NeuralJobService(INeuralJobRepository neuralJobRepository, IMsgProducer eventProducer)
        {
            _neuralJobRepository = neuralJobRepository;
            _eventProducer = eventProducer;
        }

        public async Task<NeuralJob> CreateNeuralJob(NeuralJobForm newJobFrom)
        {
            var job = new NeuralJob { Name = newJobFrom.Name, Status = NeuralJobStatus.IDLE };
            await _neuralJobRepository.InsertAsync(job);
            return job;            
        }

        public async Task TrainNeuralJob(Guid jobId)
        {
            var job = await _neuralJobRepository.GetByIdAsync(jobId);
            _eventProducer.AddEventToQueue<NeuralJob>(Queues.NN_START_TRAIN, job);
        }
    }
}
