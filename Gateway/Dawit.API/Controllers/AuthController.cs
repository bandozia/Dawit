using Dawit.Infrastructure.Repositories;
using Dawit.Infrastructure.Service.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawit.API.Model.Form;
using Microsoft.AspNetCore.Authorization;

namespace Dawit.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            string token = await _userService.AuthenticateUserAsync(form.Email, form.Password);
            if (token is not null)
                return Ok(token);
            else
                return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                //TODO: implement specialized exceptions in service
                try
                {
                    await _userService.CreateUserAsync(form.Email, form.Password);
                    return Ok();
                }
                catch 
                {                    
                    return Problem("user ja existe");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("validate")]
        [Authorize]
        public IActionResult Validate()
        {
            return Ok("to be removed");
        }

    }
}
