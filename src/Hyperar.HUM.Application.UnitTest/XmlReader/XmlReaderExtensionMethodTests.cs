namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class XmlReaderExtensionMethodTests
    {
        [Theory]
        [InlineData("<Node />", "node")]
        [InlineData("<NODE />", "node")]
        [InlineData("<node />", "node")]
        [InlineData("<nODE />", "node")]
        public async Task ReadStringValueAsXmlReader_NodeNameShouldBeEqual(string xmlContent, string value)
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                using (var xmlReader = XmlReader.Create(memoryStream, new XmlReaderSettings { Async = true }))
                {
                    await xmlReader.ReadAsync();

                    Assert.True(xmlReader.CheckNode(value));
                }
            }
        }

        [Theory]
        [InlineData("<Node />", null)]
        [InlineData("<Node></Node>", "")]
        [InlineData("<Node AttributeName=\"value\" />", null)]
        [InlineData("<Node AttributeName=\"value\">Value</Node>", "Value")]
        public async Task ReadStringValueAsXmlReader_NodeValueShouldBeEqual(string xmlContent, string? value)
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                using (var xmlReader = XmlReader.Create(memoryStream, new XmlReaderSettings { Async = true }))
                {
                    xmlReader.ReadToFollowing("Node");

                    Assert.Equal((await xmlReader.ReadValueAsync()).AsNullableString(), value);
                }
            }
        }

        [Theory]
        [InlineData("<Node><ChildNode /></Node>")]
        [InlineData("<Node><ChildNode></ChildNode></Node>")]
        public async Task ReadStringValueAsXmlReader_ShouldThrowBusinessException(string xmlContent)
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                using (var xmlReader = XmlReader.Create(memoryStream, new XmlReaderSettings { Async = true }))
                {
                    xmlReader.ReadToFollowing("Node");

                    await Assert.ThrowsAsync<XmlException>(xmlReader.ReadValueAsync);
                }
            }
        }
    }
}