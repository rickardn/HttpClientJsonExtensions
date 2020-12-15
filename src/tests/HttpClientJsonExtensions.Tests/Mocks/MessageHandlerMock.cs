using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientJsonExtensions.Specs
{
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
}