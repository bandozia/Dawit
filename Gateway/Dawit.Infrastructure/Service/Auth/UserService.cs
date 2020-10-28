using Dawit.Domain.Model.Auth;
using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Service.Extensions;
using System;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Auth
{
    public class UserService : IUserService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IAppUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return password.IsEqualsHashed(user?.Password) switch
            {
                true => _tokenService.GenerateUserToken(user),
                _ => null
            };                                    
        }

        public async Task CreateUserAsync(string email, string password)
        {
            var user = new AppUser { Email = email, Password = password.ToHashed(), CreationDate = DateTime.UtcNow };
            await _userRepository.InsertAsync(user);
        }
    }
}
