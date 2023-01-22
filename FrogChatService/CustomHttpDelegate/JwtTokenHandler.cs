using ClientStorage;
namespace FrogChatService.CustomHttpDelegate
{
    public class JwtTokenHandler : DelegatingHandler
    {
        private readonly IClientStorage _clientStorage;

        public JwtTokenHandler(IClientStorage clientStorage) 
        {
            _clientStorage = clientStorage;
        }
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {await _clientStorage.GetValue<string>("Token")}");
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
