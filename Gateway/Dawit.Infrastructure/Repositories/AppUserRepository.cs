using Dawit.Domain.Model.Auth;
using Dawit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>
    {
        public AppUserRepository(BaseContext context) : base(context)
        {
        }
    }
}
