using Challenge.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Response
{
    public class LoginResponse
    {
        public bool status = true;
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }

    }
}
