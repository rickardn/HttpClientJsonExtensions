using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace HttpClientJsonExtensions.Tests
{
    public class HttpClientJsonScenarios
    {
        private const string JsonMediaType = "application/json";
        private MessageHandlerMock _messageHandler;
        private HttpClient _httpClient;

        [SetUp]
        public void BeforeEach()
        {
            _messageHandler = new MessageHandlerMock();
            _httpClient = new HttpClient(_messageHandler)
            {
                BaseAddress = new Uri("http://localhost")
            };
        }

        [Test]
        public async Task ShouldReturnDeserializedObject()
        {
            _messageHandler.ReturnsForAnyArgs(
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

        [Test]
        public async Task ShouldReturnHttpResponseMessage()
        {
            _messageHandler.ReturnsForAnyArgs(new HttpResponseMessage(HttpStatusCode.OK));

            var response = await _httpClient.PostAsync("/", new Foo{Prop = "value"});

            _messageHandler.Request.Method.ShouldBe(HttpMethod.Post);
            _messageHandler.Request.Content.Headers.ContentType.MediaType.ShouldBe(JsonMediaType);
            _messageHandler.Request.Content.ReadAsStringAsync().Result.ShouldBe(@"{""Prop"":""value""}");
        }
    }

    public class MessageHandlerMock : HttpMessageHandler
    {
        private HttpResponseMessage _returns;

        public void ReturnsForAnyArgs(HttpResponseMessage returns)
        {
            _returns = returns;
        }

        public HttpRequestMessage Request { get; set; }

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