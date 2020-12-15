using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace HttpClientJsonExtensions.Specs
{
    public class SendAsyncSpec : HttpClientJsonExtensionSpec
    {
        [Test]
        public async Task ShouldReturnDeserializedObject()
        {
            GivenAResponse(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        @"{ ""prop"":""value"" }",
                        Encoding.UTF8,
                        JsonMediaType)
                });

            var request = new HttpRequestMessage();
            var response = await _httpClient.SendAsync<Foo>(request);

            response.Prop.ShouldBe("value");
        }
    }
}