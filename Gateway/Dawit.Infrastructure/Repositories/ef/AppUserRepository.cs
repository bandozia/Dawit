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
    public class AppUserRepository : BasicRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(BaseContext context) : base(context)
        {
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<AppUser> InsertAsync(AppUser user)
        {
            DbSet.Add(user);
            await Context.SaveChangesAsync();
            return user;
        }
    }
}
