using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace HttpClientJsonExtensions.Tests
{
    public class HttpClientJsonExtensionSpec
    {
        protected const string JsonMediaType = "application/json";
        protected const string SerializedObject = @"{""prop"":""value""}";
        protected HttpClient _httpClient;
        protected MessageHandlerMock _messageHandler;

        [SetUp]
        public void BeforeEach()
        {
            _messageHandler = new MessageHandlerMock();
            _httpClient = new HttpClient(_messageHandler)
            {
                BaseAddress = new Uri("http://localhost")
            };
        }

        protected void GivenAResponse(HttpResponseMessage httpResponseMessage)
        {
            _messageHandler.ReturnsForAnyArgs(httpResponseMessage);
        }
    }

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

    public class DeleteAsyncSpec : HttpClientJsonExtensionSpec
    {
        [Test]
        public async Task ShouldReturnDeserializedObject()
        {
            GivenAResponse(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        SerializedObject,
                        Encoding.UTF8,
                        JsonMediaType)
                });

            var response = await _httpClient.DeleteAsync<Foo>("/");

            response.Prop.ShouldBe("value");
        }
    }

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

    public class PatchAsyncSpec : HttpClientJsonExtensionSpec
    {
        [Test]
        public async Task ShouldSendObjectSerializedAsJson()
        {
            GivenAResponse(new HttpResponseMessage(HttpStatusCode.OK));

            await _httpClient.PatchAsync("/", new Foo { Prop = "value" });

            _messageHandler.Request.ShouldBeRequest(
                withMethod: HttpMethod.Patch,
                withContent: SerializedObject);
        }
    }

    public class WithContentSpec : HttpClientJsonExtensionSpec
    {
        [Test]
        public void ShouldAddJsonContent()
        {
            var requestMessage = new HttpRequestMessage()
                .WithContent(new Foo{Prop = "value"});
            requestMessage.Content.ReadAsStringAsync().Result.ShouldBe(SerializedObject);
        }
    }

    public static class ShouldlyHttpRequestMessageExtensions
    {
        public static void ShouldBeRequest(this HttpRequestMessage request, HttpMethod withMethod, string withContent)
        {
            request.Method.ShouldBe(withMethod);
            request.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
            request.Content.ReadAsStringAsync().Result.ShouldBe(withContent);
        }
    }

    public class MessageHandlerMock : HttpMessageHandler
    {
        private HttpResponseMessage _returns;

        public HttpRequestMessage Request { get; set; }

        public void ReturnsForAnyArgs(HttpResponseMessage returns)
        {
            _returns = returns;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Request = request;
            return await Task.FromResult(_returns);
        }
    }

    public class Foo
    {
        public string Prop { get; set; }
    }
}