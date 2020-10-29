using Dawit.Domain.Model.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Neural
{
    public interface INeuralNetworkService
    {
        Task<NeuralNetwork> CreateNeuralNetwork(NeuralNetwork network);
        Task<bool> StartTrainNetwork(string networkId);
    }
}
