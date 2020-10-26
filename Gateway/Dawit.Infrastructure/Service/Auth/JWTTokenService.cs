using Dawit.Domain.Model.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Service.Auth
{
    public class JWTTokenService : ITokenService
    {
        private readonly string _secret;
        private readonly int _duration;

        public JWTTokenService(string tokenSecret, int duration)
        {
            _secret = tokenSecret;
            _duration = duration;
        }

        public string GenerateUserToken(AppUser user)
        {
            byte[] key = Encoding.UTF8.GetBytes(_secret);
            var tokenHandle = new JsonWebTokenHandler();

            var claims = new Dictionary<string, object>
            {
                {nameof(user.Email), user.Email}
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = DateTime.UtcNow.AddHours(_duration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandle.CreateToken(tokenDescriptor);
        }
    }
}
