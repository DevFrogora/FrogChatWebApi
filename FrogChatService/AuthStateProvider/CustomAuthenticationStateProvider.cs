using Blazored.LocalStorage;
using ClientStorage;
using FrogChatModel.DTOModel;
using FrogChatService.WebApiUtils;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FrogChatService.AuthStateProvider
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState _anonymousUser = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private AuthenticationState currentUser = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        public CustomAuthenticationStateProvider() { }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            #region BadIdea
            //var token = await clientStorage.GetItemAsStringAsync("Token");
            //if(string.IsNullOrEmpty(token))
            //{
            //     return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            //}
            #endregion
            return currentUser;
        }

        public void NotifyOnUserAutentication(string token)
        {
            var identity = new ClaimsIdentity(GetCliam(token), "bearer");
            currentUser = new AuthenticationState(new ClaimsPrincipal(identity));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyOnUserLogout()
        {
            currentUser = _anonymousUser;
        }

        IEnumerable<Claim> GetCliam(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            return tokenS.Claims;
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            if (jsonToken.ValidTo > DateTime.UtcNow.AddSeconds(3))
            {
                return true;
            }
            return false;
        }
    }
}

//new[]{
//                new Claim(ClaimTypes.Name , user.Name),
//                new Claim(ClaimTypes.Email , user.Email),
//                new Claim("picture" , user.PhotoUrl)

//            }


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