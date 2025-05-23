﻿namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
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
            Avatar? result = null;

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                result = new Avatar(
                    (await xmlReader.ReadValueAsync()).AsString(),
                    await ReadLayerNodes(
                        xmlReader));
            }

            await xmlReader.ReadAsync();

            return result;
        }

        protected static async Task<IdName[]> ReadIdNameListNodeAsync(
            XmlReader xmlReader,
            string expectedName,
            string expectedChildName)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        expectedName,
                        xmlReader.Name));
            }

            return await ReadIdNameListNodeInternalAsync(
                xmlReader,
                expectedChildName);
        }

        protected static async Task<IdName> ReadIdNameNodeAsync(
            XmlReader xmlReader,
            string expectedName)
        {
            if (!xmlReader.CheckNode(expectedName))
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        expectedName,
                        xmlReader.Name));
            }

            if (xmlReader.IsEmptyElement)
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.UnexpectedEmptyElement,
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
            IdName[]? result = null;

            if (!xmlReader.CheckNode(expectedName))
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        expectedName,
                        xmlReader.Name));
            }

            if (xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();
            }
            else
            {
                result = await ReadIdNameListNodeInternalAsync(
                    xmlReader,
                    expectedChildName);
            }

            return result;
        }

        protected static async Task<IdName?> ReadNullableIdNameNodeAsync(
            XmlReader xmlReader,
            string expectedName)

        {
            if (!xmlReader.CheckNode(expectedName))
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        expectedName,
                        xmlReader.Name));
            }

            IdName? node = null;

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                node = new IdName(
                    (await xmlReader.ReadValueAsync()).AsLong(),
                    (await xmlReader.ReadValueAsync()).AsString());
            }

            await xmlReader.ReadAsync();

            return node;
        }

        private static async Task<IdName[]> ReadIdNameListNodeInternalAsync(
            XmlReader xmlReader,
            string expectedChildName)
        {
            var nodes = new List<IdName>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(expectedChildName))
                {
                    nodes.Add(
                        await ReadIdNameNodeAsync(
                            xmlReader,
                            expectedChildName));
                }
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