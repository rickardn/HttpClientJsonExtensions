using System.Net.Http;
using NUnit.Framework;
using Shouldly;

namespace HttpClientJsonExtensions.Specs
{
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
}