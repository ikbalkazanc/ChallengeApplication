using Challenge.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Services
{
    public interface IUserService
    {
        public Task<UserDto> CreateUserAsync(RegisterDto createUserDto);
    }
}
