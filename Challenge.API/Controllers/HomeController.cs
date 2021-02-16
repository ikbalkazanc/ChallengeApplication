using Challenge.Core.Redis;
using Microsoft.AspNetCore.Authorization;
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
    public class HomeController : ControllerBase
    {
        private readonly IRedisService _redis;
        public HomeController(IRedisService redis)
        {
            _redis = redis;
        }
        [HttpGet]
       
        public async Task<IActionResult> Index()
        {
            
            return Ok("sa");
        }
    }
}
