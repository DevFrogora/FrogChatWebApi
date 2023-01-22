﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatService.WebApiUtils
{
    public static class QueryIdentity
    {
        public static string? GetClaimValue(ClaimsPrincipal claimsPrincipal,string claimType)
        {
           return claimsPrincipal.Claims.Where(claim => claim.Type == ClaimTypes.Email).Select(claim => claim.Value).FirstOrDefault();
        }
        public static string? GetClaimValue(IEnumerable<Claim> Claims, string claimType)
        {
            return Claims.Where(claim => claim.Type == ClaimTypes.Email).Select(claim => claim.Value).FirstOrDefault();
        }
    }
}
