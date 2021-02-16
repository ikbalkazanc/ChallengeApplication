using Challenge.Core.DTO;
using Challenge.Core.Models;
using Challenge.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ChallengeUser> _userManager;

        public UserService(UserManager<ChallengeUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserDto> CreateUserAsync(RegisterDto createUserDto)
        {
            var user = new ChallengeUser {Id= Convert.ToBase64String(Encoding.UTF8.GetBytes(createUserDto.UserName)), Name = createUserDto.Name, LastName = createUserDto.LastName, Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return new UserDto();
            }
            return ObjectMapper.Mapper.Map<UserDto>(user);
        }
    }
}
