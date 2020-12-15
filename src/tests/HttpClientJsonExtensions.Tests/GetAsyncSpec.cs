using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace HttpClientJsonExtensions.Specs
{
    public class GetAsyncSpec : HttpClientJsonExtensionSpec
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

            var response = await _httpClient.GetAsync<Foo>("/");

            response.Prop.ShouldBe("value");
        }
    }
}