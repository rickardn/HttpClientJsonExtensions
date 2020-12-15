using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace HttpClientJsonExtensions
{
    public static class HttpResponseMessageJsonExtensions
    {
        /// <summary>
        ///     Get a deserialized object from a <see cref="HttpResponseMessage"/> JSON content.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/></param>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        public static T Content<T>(this HttpResponseMessage response)
        {
            Stream stream = response.Content.ReadAsStreamAsync().Result;
            var jsonReader = new JsonTextReader(new StreamReader(stream));
            return new JsonSerializer().Deserialize<T>(jsonReader);
        }
    }
}