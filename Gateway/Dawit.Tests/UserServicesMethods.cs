﻿using Dawit.Infrastructure.Context;
using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Repositories.ef;
using Dawit.Infrastructure.Service.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dawit.Tests
{
    public class UserServicesMethods
    {        
        private readonly IUserService userService;
        private readonly IAppUserRepository userRepo;

        public UserServicesMethods()
        {
            var testContext = TestContextUtils.CreateInMemoryDbContext();
            userRepo = new AppUserRepository(testContext);
            var tokenService = new JWTTokenService("jfghbk@on345_38*b6b@ac8asdffafg5774a&%67454378*&092mdf", 5);
            userService = new UserService(userRepo, tokenService);
        }

        [Fact(DisplayName = "CreateUser include user in db with hashed pass")]        
        public async Task CreateUserWorks()
        {
            await userService.CreateUserAsync("fake@mail.com", "thepassword");
            var user = await userRepo.GetByEmailAsync("fake@mail.com");
            Assert.NotNull(user);
            Assert.Equal(48, user.Password.Length);
        }

        [Fact(DisplayName = "Authenticate created user and return token")]        
        public async Task AuthUsers()
        {
            await userService.CreateUserAsync("fake2@mail.com", "thepassword");

            string validToken = await userService.AuthenticateUserAsync("fake2@mail.com", "thepassword");
            string wrongPass = await userService.AuthenticateUserAsync("fake2@mail.com", "wrongpass");
            string unexistent = await userService.AuthenticateUserAsync("fake666@mail.com", "thepassword");

            Assert.NotNull(validToken);
            Assert.Null(wrongPass);
            Assert.Null(unexistent);
        }

        
        
        
        
    }
}
