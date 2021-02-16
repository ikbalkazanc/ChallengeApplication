using AspNetCore.Identity.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Models
{
    public class ChallengeUser : MongoUser<string>
    {
        public string Name { get; set; }
        public string  LastName { get; set; }
    }
}
