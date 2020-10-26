using Dawit.Domain.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetByEmailAsync(string email);
        Task<AppUser> InsertAsync(AppUser user);
    }
}
