using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public interface ITokenService
    {

        bool ValidateToken(string token);
        IEnumerable<Claim> GetClaims(string token);
    }
}
