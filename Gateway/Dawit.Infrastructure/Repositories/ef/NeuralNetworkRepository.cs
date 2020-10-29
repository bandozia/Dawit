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
    public class NeuralNetworkRepository : BasicRepository<NeuralNetwork>, INeuralNetworkRepository
    {
        public NeuralNetworkRepository(BaseContext context) : base(context)
        {
        }

        public async Task<NeuralNetwork> GetByIdAsync(Guid id)
        {
            return await DbSet.SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<NeuralNetwork> InsertAsync(NeuralNetwork item)
        {
            DbSet.Add(item);
            await Context.SaveChangesAsync();
            return item;
        }
    }
}
