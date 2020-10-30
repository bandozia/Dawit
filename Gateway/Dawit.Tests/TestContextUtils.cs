using Dawit.API.Service.Signal;
using Dawit.Infrastructure.Context;
using Dawit.Infrastructure.Repositories.ef;
using Dawit.Infrastructure.Service.Auth;
using Dawit.Infrastructure.Service.Signal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Tests
{
    static class TestContextUtils
    {
        internal static BaseContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase("tests")
                .Options;

            return new BaseContext(options);
        }

        internal static IConnectionMapping<Guid> ConnectionMapperContext()
        {
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            var memCacheMapping = new MemoryCacheMapping(new MemoryDistributedCache(opts));
            return memCacheMapping;
        }
               
    }
}
