using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrogChatService
{
    public static class HttpClientExtension
    {
        public static async  Task<HttpResponseMessage?> PutJsonAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            return await client.PutAsJsonAsync<TValue>(requestUri, value, options, cancellationToken);
            //response.EnsureSuccessStatusCode();

            //return await response.Content.ReadFromJsonAsync<HttpResponseMessage>();
        }

        public static async Task<TValue?> PostJsonAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsJsonAsync<TValue>(requestUri, value, options, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TValue>();
        }

        public static async Task<TValue?> DeletetJsonAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri)
        {
            return await client.DeleteFromJsonAsync<TValue>(requestUri);
        }
    }
}
