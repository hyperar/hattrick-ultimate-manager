namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public class XmlFileParser : IXmlFileParser
    {
        private readonly IFileParseStrategyFactory fileParseStrategyFactory;

        public XmlFileParser(IFileParseStrategyFactory fileParseStrategyFactory)
        {
            this.fileParseStrategyFactory = fileParseStrategyFactory;
        }

        public async Task<IXmlFileBase> ParseXmlFileAsync(byte[] fileContent, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream(fileContent))
            {
                var xmlReaderSettings = new XmlReaderSettings
                {
                    Async = true,
                    CloseInput = true,
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    IgnoreWhitespace = true
                };

                using (var xmlReader = XmlReader.Create(memoryStream, xmlReaderSettings))
                {
                    xmlReader.ReadToFollowing(NodeName.FileName);

                    var fileName = (await xmlReader.ReadValueAsync()).AsString();
                    var version = (await xmlReader.ReadValueAsync()).AsDecimal();
                    var userId = (await xmlReader.ReadValueAsync()).AsLong();
                    var fetchedDate = (await xmlReader.ReadValueAsync()).AsDateTime();

                    var parser = this.fileParseStrategyFactory.GetFor(fileName);

                    return await parser.ExecuteFileParseAsync(
                        xmlReader,
                        fileName,
                        version,
                        userId,
                        fetchedDate,
                        cancellationToken);
                }
            }
        }
    }
}