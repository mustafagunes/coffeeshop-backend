using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Service.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
    }
}