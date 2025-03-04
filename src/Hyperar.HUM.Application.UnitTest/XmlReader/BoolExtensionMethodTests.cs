namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class BoolExtensionMethodTests
    {
        [Theory]
        [InlineData("true", true)]
        [InlineData("True", true)]
        [InlineData("TRUE", true)]
        [InlineData("tRUE", true)]
        [InlineData("1", true)]
        [InlineData("false", false)]
        [InlineData("False", false)]
        [InlineData("FALSE", false)]
        [InlineData("fALSE", false)]
        [InlineData("0", false)]
        public void ReadStringValueAsBool_ShouldBeEqual(string value, bool compareValue)
        {
            Assert.Equal(value.AsBool(), compareValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsBool_ShouldThrowArgumentException(string? value)
        {
            Assert.Throws<ArgumentException>(() => value.AsBool());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsBool_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => value.AsBool());
        }

        [Theory]
        [InlineData("2")]
        [InlineData("invalid value")]
        public void ReadStringValueAsBool_ShouldThrowFormatException(string? value)
        {
            Assert.Throws<FormatException>(() => value.AsBool());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsNullableBool_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableBool());
        }
    }
}