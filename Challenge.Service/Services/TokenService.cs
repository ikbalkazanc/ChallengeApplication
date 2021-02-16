using Challenge.Core.Models;
using Challenge.Core.Response;
using Challenge.Core.Services;
using Challenge.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ChallengeUser> _userManager;
        private readonly CustomTokenOptions _tokenOption;
        public TokenService(UserManager<ChallengeUser> userManager, IOptions<CustomTokenOptions> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }
        private IEnumerable<Claim> GetClaims(ChallengeUser userApp, List<String> audiences)
        {
            //email payloading to inside of jwt
            var userList = new List<Claim> {
                //new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }

        public LoginResponse CreateToken(ChallengeUser userApp)
        {
            var accessTokenExpriation = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpriation,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new LoginResponse
            {
                AccessTokenExpiration = accessTokenExpriation,
                AccessToken = token,
            };
            return tokenDto;
        }


    }
}
