using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HttpClientJsonExtensions
{
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