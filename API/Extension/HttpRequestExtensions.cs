using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CoffeeShop.API.Extension
{
    public static class HttpRequestExtensions
    {
        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
    }
}