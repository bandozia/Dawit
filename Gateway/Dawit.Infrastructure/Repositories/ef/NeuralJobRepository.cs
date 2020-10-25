using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories.ef
{
    public class NeuralJobRepository : BaseRepository<NeuralJob>
    {
        public NeuralJobRepository(BaseContext context) : base(context)
        {
        }

        public override Task<NeuralJob> GetByIdAsync(Guid id)
        {
            return DbSet.Include(n => n.Metrics).SingleAsync(n => n.Id == id);
        }

    }
}
