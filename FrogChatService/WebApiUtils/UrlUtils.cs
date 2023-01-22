using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.WebUtilities;

namespace FrogChatService.WebApiUtils
{
    public class UrlUtils
    {
        public static string GetTokenFromUrl(Uri absoluteUri)
        {
            
            if (QueryHelpers.ParseQuery(absoluteUri.Query).TryGetValue("access_token", out var _accessToken))
            {
                if (_accessToken.Equals("UnAuthorized"))
                {
                    return string.Empty;
                }
                return _accessToken;
            }
            return string.Empty;
        }
    }
}
