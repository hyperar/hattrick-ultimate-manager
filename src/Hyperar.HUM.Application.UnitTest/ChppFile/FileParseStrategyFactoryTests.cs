namespace Hyperar.HUM.Application.UnitTest.ChppFile
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class FileParseStrategyFactoryTests : IClassFixture<ServicesFixture>
    {
        private readonly IFileParseStrategyFactory fileParseStrategyFactory;

        public FileParseStrategyFactoryTests(ServicesFixture fixture)
        {
            this.fileParseStrategyFactory = fixture.Services.GetRequiredService<IFileParseStrategyFactory>();
        }

        [Theory]
        [InlineData(FileName.CheckToken, typeof(CheckTokenParser))]
        [InlineData(FileName.Error, typeof(ErrorParser))]
        [InlineData(FileName.ManagerCompendium, typeof(ManagerCompendiumParser))]
        [InlineData(FileName.WorldDetails, typeof(WorldDetailsParser))]
        public void FileParseStrategyFactoryImplementedXmlFileType_ShouldBeOfType(string fileName, Type returnType)
        {
            var strategy = this.fileParseStrategyFactory.GetFor(fileName);

            Assert.IsType(returnType, strategy);
        }

        [Theory]
        [InlineData(FileName.Achievements)]
        [InlineData(FileName.YouthTeamDetails)]
        public void FileParseStrategyFactoryNotImplementedXmlFileType_ShouldThrowArgumentOutOfRangeException(string fileName)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.fileParseStrategyFactory.GetFor(fileName));
        }
    }
}