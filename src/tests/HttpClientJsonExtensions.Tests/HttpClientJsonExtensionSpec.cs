// ReSharper disable InconsistentNaming
using System;
using System.Net.Http;
using NUnit.Framework;

namespace HttpClientJsonExtensions.Specs
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
}