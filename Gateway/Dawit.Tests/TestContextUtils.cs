using Dawit.Infrastructure.Context;
using Dawit.Infrastructure.Repositories.ef;
using Dawit.Infrastructure.Service.Auth;
using Microsoft.EntityFrameworkCore;
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
               
    }
}
