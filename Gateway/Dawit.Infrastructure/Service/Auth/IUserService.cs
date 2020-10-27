using Dawit.Domain.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Auth
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(string email, string password);
        Task CreateUser(string email, string password);
    }
}
