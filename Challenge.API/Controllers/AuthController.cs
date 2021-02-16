using Challenge.Core.DTO;
using Challenge.Core.Models;
using Challenge.Core.Response;
using Challenge.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService,  IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            await _userService.CreateUserAsync(model);
            return Ok();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authenticationService.CreateTokenAsync(model);
            return Ok(result);
        }
    }
}
