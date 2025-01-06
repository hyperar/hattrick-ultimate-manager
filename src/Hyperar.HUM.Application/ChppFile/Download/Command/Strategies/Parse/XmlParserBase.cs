namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Models.Chpp;

    public abstract class XmlParserBase
    {
        protected static async Task<Avatar?> ReadAvatarNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await xmlReader.ReadAsync();

            var result = new Avatar(
                await xmlReader.ReadStringAsync(),
                await ReadLayerNodes(
                    xmlReader,
                    cancellationToken));

            // Reads closing node.
            await xmlReader.ReadAsync();

            return result;
        }

        protected static async Task<IEnumerable<IdName>> ReadIdNameListNodeAsync(
            XmlReader xmlReader,
            string expectedName,
            string expectedChildName,
            CancellationToken cancellationToken)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                throw new Exception($"Unexpected Node. Expected: {expectedName}. Found: {xmlReader.Name}");
            }

            return await ReadIdNameListNodeInternalAsync(
                xmlReader,
                expectedName,
                expectedChildName,
                cancellationToken);
        }

        protected static async Task<IdName> ReadIdNameNodeAsync(
            XmlReader xmlReader,
            string expectedName,
            CancellationToken cancellationToken)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                throw new Exception($"Unexpected Node. Expected: {expectedName}. Found: {xmlReader.Name}");
            }

            await xmlReader.ReadAsync();

            var node = new IdName(
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadStringAsync());

            await xmlReader.ReadAsync();

            return node;
        }

        protected static async Task<IEnumerable<IdName>?> ReadNullableIdNameListNodeAsync(
            XmlReader xmlReader,
            string expectedName,
            string expectedChildName,
            CancellationToken cancellationToken)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                return null;
            }

            return await ReadIdNameListNodeInternalAsync(
                xmlReader,
                expectedName,
                expectedChildName,
                cancellationToken);
        }

        protected static async Task<IdName?> ReadNullableIdNameNodeAsync(
            XmlReader xmlReader,
            string expectedName,
            CancellationToken cancellationToken)

        {
            if (xmlReader.IsEmptyElement)
            {
                return null;
            }
            else if (!xmlReader.CheckNode(expectedName))
            {
                throw new Exception(
                    string.Format(
                        Globalization.ErrorMessages.UnexpectedNode,
                        expectedName,
                        xmlReader.Name));
            }

            await xmlReader.ReadAsync();

            var node = new IdName(
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadStringAsync());

            await xmlReader.ReadAsync();

            return node;
        }

        private static async Task<IEnumerable<IdName>> ReadIdNameListNodeInternalAsync(
            XmlReader xmlReader,
            string expectedName,
            string expectedChildName,
            CancellationToken cancellationToken)
        {
            var nodes = new List<IdName>();

            await xmlReader.ReadAsync();

            while (xmlReader.CheckNode(expectedChildName))
            {
                nodes.Add(
                    await ReadIdNameNodeAsync(
                        xmlReader,
                        expectedChildName,
                        cancellationToken));
            }

            await xmlReader.ReadAsync();

            return nodes;
        }

        private static async Task<Layer> ReadLayerNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            var x = int.Parse(xmlReader.GetAttribute(NodeName.X) ?? "0");
            var y = int.Parse(xmlReader.GetAttribute(NodeName.Y) ?? "0");

            await xmlReader.ReadAsync();

            var result = new Layer(
                x,
                y,
                await xmlReader.ReadStringAsync());

            await xmlReader.ReadAsync();

            return result;
        }

        private static async Task<IEnumerable<Layer>> ReadLayerNodes(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            var nodes = new List<Layer>();

            while (xmlReader.CheckNode(NodeName.Layer))
            {
                nodes.Add(
                    await ReadLayerNodeAsync(
                        xmlReader,
                        cancellationToken));
            }

            return nodes;
        }
    }
}