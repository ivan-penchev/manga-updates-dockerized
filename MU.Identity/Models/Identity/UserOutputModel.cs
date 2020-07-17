using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Identity.Models.Identity
{
    public class UserOutputModel
    {
        public UserOutputModel(string token)
        {
            this.Token = token;
        }

        public string Token { get; }
    }
}
