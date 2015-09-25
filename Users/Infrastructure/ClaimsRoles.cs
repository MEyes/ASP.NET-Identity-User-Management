using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Users.Infrastructure
{
    public class ClaimsRoles
    {
        public static IEnumerable<Claim> CreateRolesFromClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
            if (user.HasClaim(x => x.Type == ClaimTypes.StateOrProvince
            && x.Issuer == "RemoteClaims" && x.Value == "上海")
            && user.HasClaim(x => x.Type == ClaimTypes.Role
            && x.Value == "Employee"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "ShStaff"));
            }
            return claims;
        }
    }
}