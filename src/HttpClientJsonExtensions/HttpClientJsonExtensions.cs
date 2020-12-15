using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientJsonExtensions
{
    public static class HttpClientJsonExtensions
    {
        /// <summary>
        ///     Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of the request content.</typeparam>
        /// <param name="client">The <see cref="HttpClient" /></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="requestObject">The HTTP request content sent to the server.</param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public static async Task<HttpResponseMessage> PostAsync<T>(
            this HttpClient client,
            string requestUri,
            T requestObject)
        {
            return await client.PostAsync(requestUri, new JsonContent(requestObject));
        }

        /// <summary>
        ///     Send a PUT request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of the request content.</typeparam>
        /// <param name="client">The <see cref="HttpClient" /></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="requestObject">The HTTP request content sent to the server.</param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public static async Task<HttpResponseMessage> PutAsync<T>(
            this HttpClient client,
            string requestUri,
            T requestObject)
        {
            return await client.PutAsync(requestUri, new JsonContent(requestObject));
        }

        /// <summary>
        ///     Send a PATCH request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of the request content.</typeparam>
        /// <param name="client">The <see cref="HttpClient" /></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="requestObject">The HTTP request content sent to the server.</param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public static async Task<HttpResponseMessage> PatchAsync<T>(
            this HttpClient client,
            string requestUri,
            T requestObject)
        {
            return await client.PatchAsync(requestUri, new JsonContent(requestObject));
        }

        /// <summary>
        ///     Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of returned content.</typeparam>
        /// <param name="client">The <see cref="HttpClient" /></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri)
        {
            return (await client.GetAsync(requestUri)).Content<T>();
        }

        /// <summary>
        ///     Send a DELETE request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of returned content.</typeparam>
        /// <param name="client">The <see cref="HttpClient" /></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public static async Task<T> DeleteAsync<T>(this HttpClient client, string requestUri)
        {
            return (await client.DeleteAsync(requestUri)).Content<T>();
        }

        /// <summary>
        ///     Send an HTTP as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type of returned content.</typeparam>
        /// <param name="client">The <see cref="HttpClient" /></param>
        /// <param name="request">The HTTP request message to send.</param>
        /// <returns>
        ///     The task object representing the asynchronous operation.
        /// </returns>
        public static async Task<T> SendAsync<T>(this HttpClient client, HttpRequestMessage request)
        {
            return (await client.SendAsync(request)).Content<T>();
        }
    }
}