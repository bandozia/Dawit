using Dawit.Domain.Model.Neural;
using System;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public interface INeuralNetworkRepository
    {
        public Task<NeuralNetwork> GetByIdAsync(Guid id);
        public Task<NeuralNetwork> InsertAsync(NeuralNetwork item);
    }
}
