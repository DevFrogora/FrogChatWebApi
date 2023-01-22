using Blazored.LocalStorage;
using ClientStorage;
using FrogChatModel.DTOModel;
using FrogChatService.WebApiUtils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
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

        public bool IsTokenValid(string token)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "",
                ValidIssuer = "",
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
            //... manual validations return false if anything untoward is discovered
            return validatedToken != null;

        }
    }
}
