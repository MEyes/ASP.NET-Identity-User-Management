using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Users.Infrastructure
{
    public static class LocationClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims=new List<Claim>();
            if (user.Name.ToLower()=="admin")
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "200000"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "上海"));
            }
            else
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "100000"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "北京"));
            }
            return claims;
        }

        private static Claim CreateClaim(string type,string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
        }
    }
}