using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HttpClientJsonExtensions.Specs
{
    public class PostAsyncSpec : HttpClientJsonExtensionSpec
    {
        [Test]
        public async Task ShouldSendObjectSerializedAsJson()
        {
            GivenAResponse(new HttpResponseMessage(HttpStatusCode.OK));
            
            await _httpClient.PostAsync("/", new Foo {Prop = "value"});

            _messageHandler.Request.ShouldBeRequest(
                withMethod: HttpMethod.Post,
                withContent: SerializedObject);
        }
    }
}