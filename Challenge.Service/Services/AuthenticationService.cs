using Challenge.Core.DTO;
using Challenge.Core.Models;
using Challenge.Core.Response;
using Challenge.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ChallengeUser> _userManager;

        public AuthenticationService(ITokenService tokenService, UserManager<ChallengeUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }
        public async Task<LoginResponse> CreateTokenAsync(LoginDto loginDto)
        {
            var result = new LoginResponse();
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                result.status = false;
                return result;
            }
            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password))) 
            {
                result.status = false;
                return result;
            } 
            var token = _tokenService.CreateToken(user);
            return token;
        }
    }
}
