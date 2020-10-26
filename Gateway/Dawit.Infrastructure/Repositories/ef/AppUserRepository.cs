using Dawit.Domain.Model.Auth;
using Dawit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories.ef
{
    public class AppUserRepository : IAppUserRepository
    {
        public async Task<AppUser> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> InsertAsync(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
