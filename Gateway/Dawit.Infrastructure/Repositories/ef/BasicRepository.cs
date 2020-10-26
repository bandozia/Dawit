using Dawit.Domain.Model;
using Dawit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories.ef
{
    public abstract class BasicRepository<T> where T : BaseModel
    {
        protected readonly BaseContext Context;
        protected readonly DbSet<T> DbSet;

        protected BasicRepository(BaseContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }                               

    }
}
