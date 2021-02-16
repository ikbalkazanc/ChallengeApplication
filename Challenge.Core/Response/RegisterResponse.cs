using Challenge.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Response
{
    public class RegisterResponse
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public UserDto User { get; set; }
    }
}
