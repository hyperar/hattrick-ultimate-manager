namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFileParser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.Avatars;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public class AvatarsUnitTests : XmlFileParserBase, IClassFixture<ServicesFixture>
    {
        private const string AvatarsNoSupporterHallOfFame = "Assets\\Xml\\Avatars\\Avatars_NoSupporter_HallOfFame.xml";

        private const string AvatarsNoSupporterPlayers = "Assets\\Xml\\Avatars\\Avatars_NoSupporter_Players.xml";

        private const string AvatarsSupporterHallOfFame = "Assets\\Xml\\Avatars\\Avatars_Supporter_HallOfFame.xml";

        private const string AvatarsSupporterPlayers = "Assets\\Xml\\Avatars\\Avatars_Supporter_Players.xml";

        private readonly IXmlFileParser XmlFileParser;

        public AvatarsUnitTests(ServicesFixture fixture)
        {
            this.XmlFileParser = fixture.Services.GetRequiredService<IXmlFileParser>();
        }

        [Fact]
        public async Task Avatars_NoSupporter_HallOfFame_ShouldBeEqual()
        {
            var fileContent = await OpenFile(AvatarsNoSupporterHallOfFame);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "Avatars.xml",
                1.1m,
                13900623,
                new DateTime(2025, 6, 8, 17, 53, 16),
                new Team(
                    834283,
                    []));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Avatars_NoSupporter_Players_ShouldBeEqual()
        { }

        [Fact]
        public async Task Avatars_Supporter_HallOfFame_ShouldBeEqual()
        {
            var fileContent = await OpenFile(AvatarsSupporterHallOfFame);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "Avatars.xml",
                1.1m,
                13900623,
                new DateTime(2025, 6, 8, 17, 53, 1),
                new Team(
                    1033530,
                    [
                        new Player(
                            470853181,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f5man1a.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f5e.png"),
                                    new Layer(24, 10, "/Img/Avatar/eyes/e36a.png"),
                                    new Layer(29, 45, "/Img/Avatar/mouths/m14c.png"),
                                    new Layer(18, 38, "/Img/Avatar/goatees/g2.png"),
                                    new Layer(18, 22, "/Img/Avatar/noses/n14mh.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f5h8h.png")
                                ])),
                        new Player(
                            470853196,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f8man2b.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f8d.png"),
                                    new Layer(26, 19, "/Img/Avatar/eyes/e36b.png"),
                                    new Layer(32, 64, "/Img/Avatar/mouths/m32b.png"),
                                    new Layer(18, 34, "/Img/Avatar/noses/n14.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f8h10i.png")
                                ])),
                        new Player(
                            470853192,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f1man2b.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f1b.png"),
                                    new Layer(24, 19, "/Img/Avatar/eyes/e24b.png"),
                                    new Layer(29, 63, "/Img/Avatar/mouths/m17b.png"),
                                    new Layer(22, 32, "/Img/Avatar/noses/n15.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f1h9a.png")
                                ])),
                        new Player(
                            470853191,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f2man1a.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f2a.png"),
                                    new Layer(24, 24, "/Img/Avatar/eyes/e18a.png"),
                                    new Layer(32, 56, "/Img/Avatar/mouths/m19c.png"),
                                    new Layer(21, 35, "/Img/Avatar/noses/n20.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f1h9a.png")
                                ])),
                        new Player(
                            470853190,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f5man2b.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f5d.png"),
                                    new Layer(24, 8, "/Img/Avatar/eyes/e30b.png"),
                                    new Layer(29, 45, "/Img/Avatar/mouths/m14b.png"),
                                    new Layer(18, 44, "/Img/Avatar/goatees/g3.png"),
                                    new Layer(16, 19, "/Img/Avatar/noses/n19.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f5h7i.png")
                                ])),
                        new Player(
                            470853186,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f7man1a.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7b.png"),
                                    new Layer(24, 7, "/Img/Avatar/eyes/e3a.png"),
                                    new Layer(32, 41, "/Img/Avatar/mouths/m31c.png"),
                                    new Layer(18, 8, "/Img/Avatar/noses/n28.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h15a.png")
                                ])),
                        new Player(
                            470853183,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f1man1c.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f1e.png"),
                                    new Layer(24, 19, "/Img/Avatar/eyes/e25a.png"),
                                    new Layer(30, 69, "/Img/Avatar/mouths/m36c.png"),
                                    new Layer(20, 31, "/Img/Avatar/noses/n12.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f1h3i.png")
                                ])),
                        new Player(
                            465332679,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof_int.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f3hof09.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f3c.png"),
                                    new Layer(24, 13, "/Img/Avatar/eyes/e25c.png"),
                                    new Layer(29, 57, "/Img/Avatar/mouths/m22c.png"),
                                    new Layer(18, 28, "/Img/Avatar/noses/n9.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f3h11b.png")
                                ])),
                        new Player(
                            470853193,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f4hof12.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4b.png"),
                                    new Layer(24, 18, "/Img/Avatar/eyes/e31a.png"),
                                    new Layer(31, 68, "/Img/Avatar/mouths/m39a.png"),
                                    new Layer(19, 55, "/Img/Avatar/goatees/g5.png"),
                                    new Layer(18, 31, "/Img/Avatar/noses/n12.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h8a.png")
                                ])),
                        new Player(
                            470853193,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f4hof12.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4b.png"),
                                    new Layer(24, 18, "/Img/Avatar/eyes/e31a.png"),
                                    new Layer(31, 68, "/Img/Avatar/mouths/m39a.png"),
                                    new Layer(19, 55, "/Img/Avatar/goatees/g5.png"),
                                    new Layer(18, 31, "/Img/Avatar/noses/n12.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h8a.png")
                                ])),
                        new Player(
                            470853188,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f7hof15.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7c.png"),
                                    new Layer(23, 4, "/Img/Avatar/eyes/e14c.png"),
                                    new Layer(32, 41, "/Img/Avatar/mouths/m33c.png"),
                                    new Layer(19, 26, "/Img/Avatar/goatees/g2.png"),
                                    new Layer(18, 10, "/Img/Avatar/noses/n39ma.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h1a.png")
                                ])),
                        new Player(
                            470853184,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_hof.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f4hof15.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4c.png"),
                                    new Layer(24, 17, "/Img/Avatar/eyes/e29c.png"),
                                    new Layer(28, 58, "/Img/Avatar/mouths/m23c.png"),
                                    new Layer(18, 56, "/Img/Avatar/goatees/g3.png"),
                                    new Layer(19, 33, "/Img/Avatar/noses/n20.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h15e.png")
                                ])),
                    ]));

            var expectedJson = JsonConvert.SerializeObject(expected);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Avatars_Supporter_Players_ShouldBeEqual()
        {
            var fileContent = await OpenFile(AvatarsSupporterPlayers);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "Avatars.xml",
                1.1m,
                13900623,
                new DateTime(2025, 6, 8, 17, 52, 53),
                new Team(
                    1033530,
                    [
                        new Player(
                            473632926,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099546/body12.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6a.png"),
                                    new Layer(26, 17, "/Img/Avatar/eyes/e36c.png"),
                                    new Layer(32, 65, "/Img/Avatar/mouths/m32c.png"),
                                    new Layer(19, 31, "/Img/Avatar/noses/n9.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h5a.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/1.png")
                                ])),
                        new Player(
                            477250058,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body2.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6a.png"),
                                    new Layer(25, 16, "/Img/Avatar/eyes/e24b.png"),
                                    new Layer(30, 66, "/Img/Avatar/mouths/m4bg.png"),
                                    new Layer(19, 33, "/Img/Avatar/noses/n14.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h3b.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/2.png")
                                ])),
                        new Player(
                            475548408,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body8.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f8a.png"),
                                    new Layer(21, 17, "/Img/Avatar/eyes/e22c.png"),
                                    new Layer(29, 62, "/Img/Avatar/mouths/m6c.png"),
                                    new Layer(20, 29, "/Img/Avatar/noses/n20.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f8h3a.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(19, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/3.png")
                                ])),
                        new Player(
                            484268746,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body2.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6d.png"),
                                    new Layer(25, 16, "/Img/Avatar/eyes/e30c.png"),
                                    new Layer(31, 63, "/Img/Avatar/mouths/m8c.png"),
                                    new Layer(19, 26, "/Img/Avatar/noses/n10.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h8i.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/4.png")
                                ])),
                        new Player(
                            484242418,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body10.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f5c.png"),
                                    new Layer(23, 10, "/Img/Avatar/eyes/e35c.png"),
                                    new Layer(32, 53, "/Img/Avatar/mouths/m33c.png"),
                                    new Layer(18, 40, "/Img/Avatar/goatees/g4.png"),
                                    new Layer(15, 9, "/Img/Avatar/noses/n32.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f5h6b.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/5.png")
                                ])),
                        new Player(
                            489794288,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body10.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4b.png"),
                                    new Layer(24, 20, "/Img/Avatar/eyes/e21b.png"),
                                    new Layer(31, 56, "/Img/Avatar/mouths/m19b.png"),
                                    new Layer(18, 57, "/Img/Avatar/goatees/g1.png"),
                                    new Layer(17, 33, "/Img/Avatar/noses/n9mc.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h2c.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/6.png")
                                ])),
                        new Player(
                            480157053,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body10.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f2c.png"),
                                    new Layer(26, 20, "/Img/Avatar/eyes/e24c.png"),
                                    new Layer(33, 67, "/Img/Avatar/mouths/m32c.png"),
                                    new Layer(22, 30, "/Img/Avatar/noses/n8.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f2h3e.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/7.png")
                                ])),
                        new Player(
                            479937443,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body6.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6b.png"),
                                    new Layer(22, 21, "/Img/Avatar/eyes/e10c.png"),
                                    new Layer(31, 65, "/Img/Avatar/mouths/m30c.png"),
                                    new Layer(19, 53, "/Img/Avatar/goatees/g1.png"),
                                    new Layer(22, 21, "/Img/Avatar/noses/n1.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h1c.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/8.png")
                                ])),
                        new Player(
                            465074104,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body9.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f5a.png"),
                                    new Layer(23, 7, "/Img/Avatar/eyes/e7a.png"),
                                    new Layer(30, 50, "/Img/Avatar/mouths/m36a.png"),
                                    new Layer(17, 19, "/Img/Avatar/noses/n9.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f5h15d.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/9.png")
                                ])),
                        new Player(
                            484285973,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body12.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4a.png"),
                                    new Layer(23, 18, "/Img/Avatar/eyes/e25c.png"),
                                    new Layer(29, 65, "/Img/Avatar/mouths/m36c.png"),
                                    new Layer(18, 32, "/Img/Avatar/noses/n25.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h2b.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/10.png")
                                ])),
                        new Player(
                            464689884,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body7.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f3b.png"),
                                    new Layer(23, 16, "/Img/Avatar/eyes/e24b.png"),
                                    new Layer(30, 66, "/Img/Avatar/mouths/m26b.png"),
                                    new Layer(18, 26, "/Img/Avatar/noses/n39.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f3h3a.png"),
                                    new Layer(5, 5, "/Img/Avatar/misc/red.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/11.png")
                                ])),
                        new Player(
                            458357074,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099546/body12.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6b.png"),
                                    new Layer(25, 16, "/Img/Avatar/eyes/e24c.png"),
                                    new Layer(29, 57, "/Img/Avatar/mouths/m14c.png"),
                                    new Layer(20, 31, "/Img/Avatar/noses/n20.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h0.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/12.png")
                                ])),
                        new Player(
                            490422526,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body2.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6c.png"),
                                    new Layer(26, 17, "/Img/Avatar/eyes/e26b.png"),
                                    new Layer(32, 65, "/Img/Avatar/mouths/m31b.png"),
                                    new Layer(19, 31, "/Img/Avatar/noses/n9.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h9b.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/13.png")
                                ])),
                        new Player(
                            490604840,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body10.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6d.png"),
                                    new Layer(25, 16, "/Img/Avatar/eyes/e27b.png"),
                                    new Layer(29, 59, "/Img/Avatar/mouths/m25b.png"),
                                    new Layer(22, 21, "/Img/Avatar/noses/n1.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h13h.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/14.png")
                                ])),
                        new Player(
                            484307141,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body12.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f9c.png"),
                                    new Layer(24, 12, "/Img/Avatar/eyes/e24a.png"),
                                    new Layer(30, 64, "/Img/Avatar/mouths/m4cg.png"),
                                    new Layer(19, 27, "/Img/Avatar/noses/n11ma.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f9h5a.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(19, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/15.png")
                                ])),
                        new Player(
                            484284158,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body10.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f6i.png"),
                                    new Layer(25, 16, "/Img/Avatar/eyes/e27c.png"),
                                    new Layer(31, 64, "/Img/Avatar/mouths/m5c.png"),
                                    new Layer(17, 18, "/Img/Avatar/noses/n30.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f6h10i.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/16.png")
                                ])),
                        new Player(
                            480894755,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body3.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4a.png"),
                                    new Layer(24, 17, "/Img/Avatar/eyes/e30a.png"),
                                    new Layer(31, 56, "/Img/Avatar/mouths/m19c.png"),
                                    new Layer(14, 28, "/Img/Avatar/noses/n35.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h5d.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/17.png")
                                ])),
                        new Player(
                            480304504,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body7.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f2i.png"),
                                    new Layer(25, 26, "/Img/Avatar/eyes/e4c.png"),
                                    new Layer(30, 59, "/Img/Avatar/mouths/m17c.png"),
                                    new Layer(17, 31, "/Img/Avatar/noses/n35.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f2h3i.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/18.png")
                                ])),
                        new Player(
                            480167757,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body2.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f5e.png"),
                                    new Layer(22, 11, "/Img/Avatar/eyes/e3c.png"),
                                    new Layer(30, 51, "/Img/Avatar/mouths/m30c.png"),
                                    new Layer(16, 19, "/Img/Avatar/noses/n19.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f5h10h.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/19.png")
                                ])),
                        new Player(
                            470853194,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body5.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f8c.png"),
                                    new Layer(25, 17, "/Img/Avatar/eyes/e2b.png"),
                                    new Layer(30, 64, "/Img/Avatar/mouths/m35b.png"),
                                    new Layer(19, 54, "/Img/Avatar/goatees/g1.png"),
                                    new Layer(22, 21, "/Img/Avatar/noses/n1me.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f8h4e.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/20.png")
                                ])),
                        new Player(
                            484221118,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body9.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7c.png"),
                                    new Layer(21, 7, "/Img/Avatar/eyes/e22c.png"),
                                    new Layer(30, 40, "/Img/Avatar/mouths/m39c.png"),
                                    new Layer(19, 29, "/Img/Avatar/goatees/g4.png"),
                                    new Layer(18, 10, "/Img/Avatar/noses/n37.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h1a.png"),
                                    new Layer(9, 135, "/Img/Avatar/misc/yellow.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/21.png")
                                ])),
                        new Player(
                            490339799,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body3.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f1e.png"),
                                    new Layer(25, 24, "/Img/Avatar/eyes/e2b.png"),
                                    new Layer(32, 59, "/Img/Avatar/mouths/m19b.png"),
                                    new Layer(21, 34, "/Img/Avatar/noses/n20.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f1h4i.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/22.png")
                                ])),
                        new Player(
                            489962757,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body4.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f4b.png"),
                                    new Layer(24, 20, "/Img/Avatar/eyes/e21b.png"),
                                    new Layer(28, 58, "/Img/Avatar/mouths/m23b.png"),
                                    new Layer(19, 55, "/Img/Avatar/goatees/g5.png"),
                                    new Layer(21, 26, "/Img/Avatar/noses/n24mb.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f4h12b.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/24.png")
                                ])),
                        new Player(
                            484648561,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body5.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7a.png"),
                                    new Layer(22, 5, "/Img/Avatar/eyes/e16c.png"),
                                    new Layer(30, 40, "/Img/Avatar/mouths/m37c.png"),
                                    new Layer(15, 0, "/Img/Avatar/noses/n33.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h8a.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/25.png")
                                ])),
                        new Player(
                            459545081,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body6.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f2b.png"),
                                    new Layer(23, 26, "/Img/Avatar/eyes/e10c.png"),
                                    new Layer(31, 68, "/Img/Avatar/mouths/m37c.png"),
                                    new Layer(20, 56, "/Img/Avatar/goatees/g1.png"),
                                    new Layer(20, 32, "/Img/Avatar/noses/n10.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f2h3d.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/26.png")
                                ])),
                        new Player(
                            470853182,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body5.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7a.png"),
                                    new Layer(26, 6, "/Img/Avatar/eyes/e36a.png"),
                                    new Layer(32, 41, "/Img/Avatar/mouths/m33c.png"),
                                    new Layer(18, 31, "/Img/Avatar/goatees/g1.png"),
                                    new Layer(17, 3, "/Img/Avatar/noses/n32.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h4d.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/28.png")
                                ])),
                        new Player(
                            482759979,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body6.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f8a.png"),
                                    new Layer(23, 13, "/Img/Avatar/eyes/e14c.png"),
                                    new Layer(32, 64, "/Img/Avatar/mouths/m28c.png"),
                                    new Layer(17, 32, "/Img/Avatar/noses/n19.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f8h1b.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/29.png")
                                ])),
                        new Player(
                            485152339,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body5.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f9a.png"),
                                    new Layer(25, 13, "/Img/Avatar/eyes/e30c.png"),
                                    new Layer(29, 65, "/Img/Avatar/mouths/m27c.png"),
                                    new Layer(19, 28, "/Img/Avatar/noses/n9.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f9h4h.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/30.png")
                                ])),
                        new Player(
                            483160439,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body9.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7c.png"),
                                    new Layer(25, 5, "/Img/Avatar/eyes/e26c.png"),
                                    new Layer(30, 40, "/Img/Avatar/mouths/m38c.png"),
                                    new Layer(19, 8, "/Img/Avatar/noses/n15.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h15e.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/31.png")
                                ])),
                        new Player(
                            483250976,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue.png"),
                                    new Layer(9, 10, "http://res.hattrick.org/kits/31/310/3100/3099543/body6.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f3d.png"),
                                    new Layer(22, 19, "/Img/Avatar/eyes/e5c.png"),
                                    new Layer(27, 57, "/Img/Avatar/mouths/m21c.png"),
                                    new Layer(19, 22, "/Img/Avatar/noses/n13.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f3h6h.png"),
                                    new Layer(83, 130, "/Img/Avatar/numbers/33.png")
                                ])),
                        new Player(
                            240078357,
                            new Avatar(
                                "/Img/Avatar/backgrounds/card1.png",
                                [
                                    new Layer(9, 10, "/Img/Avatar/backgrounds/bg_blue_int.png"),
                                    new Layer(9, 10, "/Img/Avatar/bodies/f7man1b.png"),
                                    new Layer(9, 10, "/Img/Avatar/faces/f7e.png"),
                                    new Layer(26, 5, "/Img/Avatar/eyes/e23c.png"),
                                    new Layer(30, 40, "/Img/Avatar/mouths/m39c.png"),
                                    new Layer(18, 10, "/Img/Avatar/noses/n37.png"),
                                    new Layer(9, 10, "/Img/Avatar/hair/f7h6h.png")
                                ]))
                    ]));

            Assert.Equal(expected, result);
        }
    }
}