using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Headers;
namespace FrogChatBlazorWAClient
{
    public class JwtTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorage;

        public JwtTokenHandler(ILocalStorageService localStorage) 
        {
            this.localStorage = localStorage;
        }
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {await localStorage.GetItemAsStringAsync("Token")}");
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
