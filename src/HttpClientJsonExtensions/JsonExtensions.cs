using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HttpClientJsonExtensions
{
    public static class HttpClientJsonExtensions
    {
        /// <summary>
        ///     Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
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

        public static async Task<HttpResponseMessage> PutAsync<T>(
            this HttpClient client,
            string requestUri,
            T requestObject)
        {
            return await client.PutAsync(requestUri, new JsonContent(requestObject));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(
            this HttpClient client,
            string requestUri,
            T requestObject)
        {
            return await client.PatchAsync(requestUri, new JsonContent(requestObject));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri)
        {
            return (await client.GetAsync(requestUri)).Content<T>();
        }

        public static async Task<T> DeleteAsync<T>(this HttpClient client, string requestUri)
        {
            return (await client.DeleteAsync(requestUri)).Content<T>();
        }

        public static async Task<T> SendAsync<T>(this HttpClient client, HttpRequestMessage request)
        {
            return (await client.SendAsync(request)).Content<T>();
        }
    }

    public static class HttpResponseMessageJsonExtensions
    {
        public static T Content<T>(this HttpResponseMessage response)
        {
            Stream stream = response.Content.ReadAsStreamAsync().Result;
            var jsonReader = new JsonTextReader(new StreamReader(stream));
            return new JsonSerializer().Deserialize<T>(jsonReader);
        }
    }

    public static class HttpRequestMessageJsonExtensions
    {
        public static HttpRequestMessage WithContent<T>(this HttpRequestMessage request, T content)
        {
            request.Content = new JsonContent(content);
            return request;
        }
    }

    internal class JsonContent : StringContent
    {
        private const string JsonContentType = "application/json";

        public JsonContent(object content)
            : base(SerializeObject(content))
        {
            Headers.ContentType = new MediaTypeHeaderValue(JsonContentType);
        }

        private static string SerializeObject(object content)
        {
            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            var stringBuilder = new StringBuilder();
            serializer.Serialize(new JsonTextWriter(new StringWriter(stringBuilder)), content);
            return stringBuilder.ToString();
        }
    }
}