namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFIleParser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.WorldDetails;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class WorldDetailsUnitTests : XmlFileParserBase, IClassFixture<ServicesFixture>
    {
        private const string WorldDetailsCountry = "Assets\\Xml\\WorldDetails\\WorldDetails_Country.xml";

        private const string WorldDetailsMultipleLeague = "Assets\\Xml\\WorldDetails\\WorldDetails_MultipleLeague.xml";

        private const string WorldDetailsNoCountry = "Assets\\Xml\\WorldDetails\\WorldDetails_NoCountry.xml";

        private const string WorldDetailsRegions = "Assets\\Xml\\WorldDetails\\WorldDetails_Regions.xml";

        private readonly IXmlFileParser xmlFileParser;

        public WorldDetailsUnitTests(ServicesFixture fixture)
        {
            this.xmlFileParser = fixture.Services.GetRequiredService<IXmlFileParser>();
        }

        [Fact]
        public async Task WorldDetails_Country_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsCountry);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "worlddetails.xml",
                1.9m,
                12345678,
                new DateTime(2025, 1, 1, 8, 15, 30),
                [
                    new League(
                        7,
                        "Argentina",
                        90,
                        0,
                        13,
                        "Argentina",
                        "South America",
                        "South America",
                        "Argentina",
                        51,
                        "Español, Rioplatense",
                        new Country(
                            true,
                            7,
                            "Argentina",
                            "Pesos",
                            10m,
                            "AR",
                            "DD/MM/YYYY",
                            "hh:mm",
                            null),
                        [
                            new Cup(2, "Copa Argentina Diego Armando Maradona", 0, 1, 1, 12, 1),
                            new Cup(518, "Copa Esmeralda Pedro 'Apache' Zurita", 0, 2, 1, 11, 2),
                            new Cup(647, "Copa Rubí Alberto Conte", 0, 2, 2, 10, 2),
                            new Cup(776, "Copa Zafiro caPiTAn_piLuSO", 0, 2, 3, 9, 2),
                            new Cup(905, "Copa Consuelo Gerardo Raúl Peludo", 0, 3, 1, 10, 2),
                            new Cup(1329, "Copa de VII División", 7, 1, 1, 7, 0),
                            new Cup(1342, "Copa Desafío Esmeralda VII División", 7, 2, 1, 7, 0),
                            new Cup(1355, "Copa Desafío Rubí VII División ", 7, 2, 2, 6, 0),
                            new Cup(1368, "Copa Desafío Zafiro VII División ", 7, 2, 3, 5, 0),
                            new Cup(1381, "Copa Consuelo VII División", 7, 3, 1, 6, 0),
                        ],
                        3006,
                        3047,
                        6751,
                        6646,
                        0,
                        new DateTime(2025, 4, 4, 2, 0, 0),
                        new DateTime(2025, 4, 5, 3, 30, 0),
                        new DateTime(2025, 4, 2, 23, 10, 0),
                        new DateTime(2025, 4, 6, 21, 40, 0),
                        new DateTime(2025, 4, 7, 11, 0, 0),
                        new DateTime(2025, 4, 8, 11, 0, 0),
                        new DateTime(2025, 4, 1, 21, 40, 0),
                        new DateTime(2025, 4, 3, 11, 0, 0),
                        new DateTime(2025, 4, 4, 19, 20, 0),
                        6)
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WorldDetails_MultipleLeague_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsMultipleLeague);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "worlddetails.xml",
                1.9m,
                12345678,
                new DateTime(2025, 1, 1, 8, 15, 30),
                [
                    new League(
                        7,
                        "Argentina",
                        90,
                        0,
                        13,
                        "Argentina",
                        "South America",
                        "South America",
                        "Argentina",
                        51,
                        "Español, Rioplatense",
                        new Country(
                            true,
                            7,
                            "Argentina",
                            "Pesos",
                            10m,
                            "AR",
                            "DD/MM/YYYY",
                            "hh:mm",
                            [
                                new IdName(67, "Catamarca"),
                                new IdName(68, "Chaco"),
                                new IdName(69, "Chubut"),
                                new IdName(70, "Ciudad Autónoma de Buenos Aires"),
                                new IdName(71, "Córdoba"),
                                new IdName(72, "Corrientes"),
                                new IdName(73, "Entre Ríos"),
                                new IdName(74, "Formosa"),
                                new IdName(75, "Jujuy"),
                                new IdName(76, "La Pampa"),
                                new IdName(77, "La Rioja"),
                                new IdName(78, "Mendoza"),
                                new IdName(79, "Misiones"),
                                new IdName(80, "Neuquén"),
                                new IdName(66, "Provincia de Buenos Aires"),
                                new IdName(81, "Río Negro"),
                                new IdName(82, "Salta"),
                                new IdName(83, "San Juan"),
                                new IdName(84, "San Luis"),
                                new IdName(85, "Santa Cruz"),
                                new IdName(86, "Santa Fe"),
                                new IdName(87, "Santiago del Estero"),
                                new IdName(88, "Tierra del Fuego, Antártida e Islas del Atlántico "),
                                new IdName(89, "Tucumán"),
                            ]),
                        [
                            new Cup(2, "Copa Argentina Diego Armando Maradona", 0, 1, 1, 12, 1),
                            new Cup(518, "Copa Esmeralda Pedro 'Apache' Zurita", 0, 2, 1, 11, 2),
                            new Cup(647, "Copa Rubí Alberto Conte", 0, 2, 2, 10, 2),
                            new Cup(776, "Copa Zafiro caPiTAn_piLuSO", 0, 2, 3, 9, 2),
                            new Cup(905, "Copa Consuelo Gerardo Raúl Peludo", 0, 3, 1, 10, 2),
                            new Cup(1329, "Copa de VII División", 7, 1, 1, 7, 0),
                            new Cup(1342, "Copa Desafío Esmeralda VII División", 7, 2, 1, 7, 0),
                            new Cup(1355, "Copa Desafío Rubí VII División ", 7, 2, 2, 6, 0),
                            new Cup(1368, "Copa Desafío Zafiro VII División ", 7, 2, 3, 5, 0),
                            new Cup(1381, "Copa Consuelo VII División", 7, 3, 1, 6, 0),
                        ],
                        3006,
                        3047,
                        6751,
                        6646,
                        0,
                        new DateTime(2025, 4, 4, 2, 0, 0),
                        new DateTime(2025, 4, 5, 3, 30, 0),
                        new DateTime(2025, 4, 2, 23, 10, 0),
                        new DateTime(2025, 4, 6, 21, 40, 0),
                        new DateTime(2025, 4, 7, 11, 0, 0),
                        new DateTime(2025, 4, 8, 11, 0, 0),
                        new DateTime(2025, 4, 1, 21, 40, 0),
                        new DateTime(2025, 4, 3, 11, 0, 0),
                        new DateTime(2025, 4, 4, 19, 20, 0),
                        6),
                    new League(
                        1003,
                        "Homegrown League",
                        3,
                        -87,
                        13,
                        "Homegrown",
                        "World",
                        "World",
                        "Homegrown League",
                        2,
                        "English (UK)",
                        new Country(
                            false,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null),
                        [
                            new Cup(1553, "Homegrown Cup", 0, 1, 1, 12, 0),
                            new Cup(1554, "Homegrown Emerald Cup", 0, 2, 1, 11, 1),
                            new Cup(1555, "Homegrown Ruby Cup", 0, 2, 2, 10, 1),
                            new Cup(1556, "Homegrown Sapphire Cup", 0, 2, 3, 9, 1),
                            new Cup(1557, "Homegrown Consolation Cup", 0, 3, 1, 10, 1)
                        ],
                        0,
                        0,
                        2233,
                        0,
                        0,
                        new DateTime(2025, 4, 4, 1, 30, 0),
                        new DateTime(2025, 4, 6, 2, 5, 0),
                        new DateTime(2025, 3, 26, 19, 45, 0),
                        new DateTime(2025, 4, 6, 19, 45, 0),
                        new DateTime(2025, 4, 7, 8, 20, 0),
                        new DateTime(2025, 4, 8, 8, 20, 0),
                        new DateTime(2025, 4, 8, 17, 20, 0),
                        new DateTime(2025, 4, 3, 8, 20, 0),
                        new DateTime(2025, 4, 5, 5, 0, 0),
                        6)
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WorldDetails_NoCountry_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsNoCountry);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "worlddetails.xml",
                1.9m,
                12345678,
                new DateTime(2025, 1, 1, 8, 15, 30),
                [
                    new League(
                        1003,
                        "Homegrown League",
                        3,
                        -87,
                        13,
                        "Homegrown",
                        "World",
                        "World",
                        "Homegrown League",
                        2,
                        "English (UK)",
                        new Country(
                            false,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null),
                        [
                            new Cup(1553, "Homegrown Cup", 0, 1, 1, 12, 0),
                            new Cup(1554, "Homegrown Emerald Cup", 0, 2, 1, 11, 1),
                            new Cup(1555, "Homegrown Ruby Cup", 0, 2, 2, 10, 1),
                            new Cup(1556, "Homegrown Sapphire Cup", 0, 2, 3, 9, 1),
                            new Cup(1557, "Homegrown Consolation Cup", 0, 3, 1, 10, 1)
                        ],
                        0,
                        0,
                        2233,
                        0,
                        0,
                        new DateTime(2025, 4, 4, 1, 30, 0),
                        new DateTime(2025, 4, 6, 2, 5, 0),
                        new DateTime(2025, 3, 26, 19, 45, 0),
                        new DateTime(2025, 4, 6, 19, 45, 0),
                        new DateTime(2025, 4, 7, 8, 20, 0),
                        new DateTime(2025, 4, 8, 8, 20, 0),
                        new DateTime(2025, 4, 8, 17, 20, 0),
                        new DateTime(2025, 4, 3, 8, 20, 0),
                        new DateTime(2025, 4, 5, 5, 0, 0),
                        6)
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task WorldDetails_Regions_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsRegions);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "worlddetails.xml",
                1.9m,
                12345678,
                new DateTime(2025, 1, 1, 8, 15, 30),
                [
                    new League(
                        7,
                        "Argentina",
                        90,
                        0,
                        13,
                        "Argentina",
                        "South America",
                        "South America",
                        "Argentina",
                        51,
                        "Español, Rioplatense",
                        new Country(
                            true,
                            7,
                            "Argentina",
                            "Pesos",
                            10m,
                            "AR",
                            "DD/MM/YYYY",
                            "hh:mm",
                            [
                                new IdName(67, "Catamarca"),
                                new IdName(68, "Chaco"),
                                new IdName(69, "Chubut"),
                                new IdName(70, "Ciudad Autónoma de Buenos Aires"),
                                new IdName(71, "Córdoba"),
                                new IdName(72, "Corrientes"),
                                new IdName(73, "Entre Ríos"),
                                new IdName(74, "Formosa"),
                                new IdName(75, "Jujuy"),
                                new IdName(76, "La Pampa"),
                                new IdName(77, "La Rioja"),
                                new IdName(78, "Mendoza"),
                                new IdName(79, "Misiones"),
                                new IdName(80, "Neuquén"),
                                new IdName(66, "Provincia de Buenos Aires"),
                                new IdName(81, "Río Negro"),
                                new IdName(82, "Salta"),
                                new IdName(83, "San Juan"),
                                new IdName(84, "San Luis"),
                                new IdName(85, "Santa Cruz"),
                                new IdName(86, "Santa Fe"),
                                new IdName(87, "Santiago del Estero"),
                                new IdName(88, "Tierra del Fuego, Antártida e Islas del Atlántico "),
                                new IdName(89, "Tucumán"),
                            ]),
                        [
                            new Cup(2, "Copa Argentina Diego Armando Maradona", 0, 1, 1, 12, 1),
                            new Cup(518, "Copa Esmeralda Pedro 'Apache' Zurita", 0, 2, 1, 11, 2),
                            new Cup(647, "Copa Rubí Alberto Conte", 0, 2, 2, 10, 2),
                            new Cup(776, "Copa Zafiro caPiTAn_piLuSO", 0, 2, 3, 9, 2),
                            new Cup(905, "Copa Consuelo Gerardo Raúl Peludo", 0, 3, 1, 10, 2),
                            new Cup(1329, "Copa de VII División", 7, 1, 1, 7, 0),
                            new Cup(1342, "Copa Desafío Esmeralda VII División", 7, 2, 1, 7, 0),
                            new Cup(1355, "Copa Desafío Rubí VII División ", 7, 2, 2, 6, 0),
                            new Cup(1368, "Copa Desafío Zafiro VII División ", 7, 2, 3, 5, 0),
                            new Cup(1381, "Copa Consuelo VII División", 7, 3, 1, 6, 0),
                        ],
                        3006,
                        3047,
                        6751,
                        6646,
                        0,
                        new DateTime(2025, 4, 4, 2, 0, 0),
                        new DateTime(2025, 4, 5, 3, 30, 0),
                        new DateTime(2025, 4, 2, 23, 10, 0),
                        new DateTime(2025, 4, 6, 21, 40, 0),
                        new DateTime(2025, 4, 7, 11, 0, 0),
                        new DateTime(2025, 4, 8, 11, 0, 0),
                        new DateTime(2025, 4, 1, 21, 40, 0),
                        new DateTime(2025, 4, 3, 11, 0, 0),
                        new DateTime(2025, 4, 4, 19, 20, 0),
                        6)
                ]);

            Assert.Equal(expected, result);
        }
    }
}