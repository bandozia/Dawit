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
    public class NeuralJobRepository : BasicRepository<NeuralJob>, INeuralJobRepository
    {
        public NeuralJobRepository(BaseContext context) : base(context)
        {
        }

        public async Task<NeuralJob> GetByIdAsync(Guid id)
        {
            return await DbSet.Include(n => n.Metrics).SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<NeuralJob> InsertAsync(NeuralJob item)
        {
            DbSet.Add(item);
            await Context.SaveChangesAsync();
            return item;
        }
    }
}
