using Dawit.Domain.Model;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Repositories.ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public interface INeuralJobRepository : IQueryRepository<NeuralJob>, ICommandRepository<NeuralJob>
    {
    }
}
