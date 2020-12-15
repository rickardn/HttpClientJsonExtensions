using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HttpClientJsonExtensions.Specs
{
    public class PutAsyncSpec : HttpClientJsonExtensionSpec
    {
        [Test]
        public async Task ShouldSendObjectSerializedAsJson()
        {
            GivenAResponse(new HttpResponseMessage(HttpStatusCode.OK));

            await _httpClient.PutAsync("/", new Foo {Prop = "value"});

            _messageHandler.Request.ShouldBeRequest(
                withMethod: HttpMethod.Put,
                withContent: SerializedObject);
        }
    }
}