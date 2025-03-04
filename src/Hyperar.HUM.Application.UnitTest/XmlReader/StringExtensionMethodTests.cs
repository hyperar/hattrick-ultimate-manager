namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class StringExtensionMethodTests
    {
        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsNullableString_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableString());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsString_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(value.AsString);
        }
    }
}