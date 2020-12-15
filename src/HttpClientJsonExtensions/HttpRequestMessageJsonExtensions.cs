using System.Net.Http;

namespace HttpClientJsonExtensions
{
    public static class HttpRequestMessageJsonExtensions
    {
        /// <summary>
        ///     Add JSON content to an HTTP request.
        /// </summary>
        /// <typeparam name="T">The type of the request content.</typeparam>
        /// <param name="request">The <see cref="HttpRequestMessage"/></param>
        /// <param name="content">The <see cref="T"/></param>
        /// <returns>
        ///     The <see cref="HttpResponseMessage"/>.
        /// </returns>
        public static HttpRequestMessage WithContent<T>(this HttpRequestMessage request, T content)
        {
            request.Content = new JsonContent(content);
            return request;
        }
    }
}