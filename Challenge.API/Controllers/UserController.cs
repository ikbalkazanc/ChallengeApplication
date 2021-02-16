using Challenge.Core.DTO;
using Challenge.Core.Models;
using Challenge.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<ChallengeUser, UserDto> _userService;
        public UserController(IService<ChallengeUser, UserDto> userService)
        {
            _userService = userService;
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto user)
        {
            await _userService.Update(user,user.Id);
            return Ok();
        }
        
    }
}
