using Challenge.Core.Models;
using Challenge.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Services
{
    public interface ITokenService
    {
        LoginResponse CreateToken(ChallengeUser userApp);
    }
}
