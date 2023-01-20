using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace FrogChatBlazorWAClient
{
    public class JwtTokenHandler : DelegatingHandler
    {
        [Inject]
        protected  ILocalStorageService localStorage { get; set; }

        public JwtTokenHandler()
        {

        }

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
