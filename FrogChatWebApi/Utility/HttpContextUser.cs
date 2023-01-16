using System.Security.Claims;

namespace FrogChatWebApi.Utility
{
    public class HttpContextUser
    {
        private readonly HttpContext context;
        ClaimsIdentity Identity { get; set; }

        public HttpContextUser(HttpContext context)
        {
            this.context = context;
            Identity = context.User.Identity as ClaimsIdentity;

        }

        public string UserName
        {
            get
            {
                return Identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            }
        }
        public string Email
        {
            get
            {
                return Identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            }
        }

        public string GetUser()
        {
            if (Identity != null)
            {
                IEnumerable<Claim> claims = Identity.Claims;
                // or
                Dictionary<string, string> claimsDic = new Dictionary<string, string>();
                foreach (var claim in claims)
                {

                    claimsDic[claim.Type] = claim.Value;
                }
            }
            return "";
        }
    }
}
