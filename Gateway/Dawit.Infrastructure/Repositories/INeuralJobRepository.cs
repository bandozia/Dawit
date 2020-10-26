using Dawit.Domain.Model.Neural;
using System;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public interface INeuralJobRepository
    {
        public Task<NeuralJob> GetByIdAsync(Guid id);
        public Task<NeuralJob> InsertAsync(NeuralJob item);
    }
}
