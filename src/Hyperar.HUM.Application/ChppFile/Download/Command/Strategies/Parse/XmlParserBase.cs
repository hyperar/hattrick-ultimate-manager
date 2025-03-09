namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Application.Exceptions;
    using Hyperar.HUM.Shared.Models.Chpp;

    public abstract class XmlParserBase
    {
        protected static async Task<Avatar?> ReadAvatarNodeAsync(XmlReader xmlReader)
        {
            // Reads opening node.
            await xmlReader.ReadAsync();

            var result = new Avatar(
                (await xmlReader.ReadValueAsync()).AsString(),
                await ReadLayerNodes(
                    xmlReader));

            // Reads closing node.
            await xmlReader.ReadAsync();

            return result;
        }

        protected static async Task<IdName> ReadIdNameNodeAsync(
            XmlReader xmlReader,
            string expectedName)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        expectedName,
                        xmlReader.Name));
            }

            await xmlReader.ReadAsync();

            var node = new IdName(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return node;
        }

        protected static async Task<IdName[]?> ReadNullableIdNameListNodeAsync(
            XmlReader xmlReader,
            string expectedName,
            string expectedChildName)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                return null;
            }

            return await ReadIdNameListNodeInternalAsync(
                xmlReader,
                expectedChildName);
        }

        protected static async Task<IdName?> ReadNullableIdNameNodeAsync(
            XmlReader xmlReader,
            string expectedName)

        {
            if (xmlReader.IsEmptyElement)
            {
                return null;
            }
            else if (!xmlReader.CheckNode(expectedName))
            {
                throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        expectedName,
                        xmlReader.Name));
            }

            await xmlReader.ReadAsync();

            var node = new IdName(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return node;
        }

        private static async Task<IdName[]> ReadIdNameListNodeInternalAsync(
            XmlReader xmlReader,
            string expectedChildName)
        {
            var nodes = new List<IdName>();

            await xmlReader.ReadAsync();

            while (xmlReader.CheckNode(expectedChildName))
            {
                nodes.Add(
                    await ReadIdNameNodeAsync(
                        xmlReader,
                        expectedChildName));
            }

            await xmlReader.ReadAsync();

            return nodes.ToArray();
        }

        private static async Task<Layer> ReadLayerNodeAsync(XmlReader xmlReader)
        {
            var x = xmlReader.GetAttribute(NodeName.X).AsInt();
            var y = xmlReader.GetAttribute(NodeName.Y).AsInt();

            await xmlReader.ReadAsync();

            var result = new Layer(
                x,
                y,
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return result;
        }

        private static async Task<Layer[]> ReadLayerNodes(XmlReader xmlReader)
        {
            var nodes = new List<Layer>();

            while (xmlReader.CheckNode(NodeName.Layer))
            {
                nodes.Add(
                    await ReadLayerNodeAsync(
                        xmlReader));
            }

            return nodes.ToArray();
        }
    }
}