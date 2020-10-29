using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Service.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Neural
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        private readonly INeuralNetworkRepository _neuralNetRepository;
        private readonly IMsgProducer _eventProducer;

        public NeuralNetworkService(INeuralNetworkRepository neuralJobRepository, IMsgProducer eventProducer)
        {
            _neuralNetRepository = neuralJobRepository;
            _eventProducer = eventProducer;
        }

        public async Task<NeuralNetwork> CreateNeuralNetwork(NeuralNetwork network)
        {
            await _neuralNetRepository.InsertAsync(network);
            return network;
        }

        public async Task<bool> StartTrainNetwork(string networkId)
        {
            if (!Guid.TryParse(networkId, out Guid id))
                return false;
            

            var network = await _neuralNetRepository.GetByIdAsync(id);

            if (network is not null)
            {                
                _eventProducer.AddEventToQueue<NeuralNetwork>(Queues.NN_START_TRAIN, network);
                return true;
            }
            else
                return false;
        }

    }
}
