using FrogChatModel.DTOModel;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FrogChatBlazorWAClient
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsPrincipal(new ClaimsIdentity()));
        public CustomAuthenticationStateProvider()
        {

        }

        public async override  Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(claimsPrincipal);
        }

        public void SetAuthInfo(UserDto user)
        {
            var identity = new ClaimsIdentity(new []{
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim("userid" , user.Id)

            }, "bearer");
            claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}


//UserDto currentUser = await httpClient.GetFromJsonAsync<UserDto>("api/user/GetCurrentUser");
//if (currentUser != null && currentUser.Email != null)
//{
//    var claim = new Claim(ClaimTypes.Name, currentUser.Email);
//    var claimIdentity = new ClaimsIdentity(new[] { claim });
//    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

//    return new AuthenticationState(claimsPrincipal);
//}
//else
//{
//    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//}