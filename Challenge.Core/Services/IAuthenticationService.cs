using Challenge.Core.DTO;
using Challenge.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> CreateTokenAsync(LoginDto loginDto);

    }
}
