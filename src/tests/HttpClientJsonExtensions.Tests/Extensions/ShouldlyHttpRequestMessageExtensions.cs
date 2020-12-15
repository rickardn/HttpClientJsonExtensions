using System.Net.Http;
using Shouldly;

namespace HttpClientJsonExtensions.Specs
{
    public static class ShouldlyHttpRequestMessageExtensions
    {
        public static void ShouldBeRequest(this HttpRequestMessage request, HttpMethod withMethod, string withContent)
        {
            request.Method.ShouldBe(withMethod);
            request.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
            request.Content.ReadAsStringAsync().Result.ShouldBe(withContent);
        }
    }
}