using Dawit.Domain.Model;
using Dawit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly BaseContext Context;
        protected readonly DbSet<T> DbSet;

        protected BaseRepository(BaseContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public virtual async Task<T> InsertAsync(T item)
        {
            DbSet.Add(item);
            await Context.SaveChangesAsync();
            return item;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

    }
}
