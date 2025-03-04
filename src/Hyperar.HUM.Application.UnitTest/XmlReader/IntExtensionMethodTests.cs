namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class IntExtensionMethodTests
    {
        [Theory]
        [InlineData("-2112147826")]
        [InlineData("-2042051368")]
        [InlineData("-1964392203")]
        [InlineData("-1707705030")]
        [InlineData("-1409329590")]
        [InlineData("-943813761")]
        [InlineData("-795447261")]
        [InlineData("-294674430")]
        [InlineData("-249093227")]
        [InlineData("-153373718")]
        [InlineData("-37879652")]
        [InlineData("618514405")]
        [InlineData("800988771")]
        [InlineData("891051852")]
        [InlineData("947924397")]
        [InlineData("951993089")]
        [InlineData("986827313")]
        [InlineData("1382073435")]
        [InlineData("1530388663")]
        [InlineData("2102339563")]
        public void ReadStringValueAsInt_ShouldBeInt(string value)
        {
            Assert.IsType<int>(value.AsInt());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsInt_ShouldThrowArgumentException(string? value)
        {
            Assert.Throws<ArgumentException>(() => value.AsInt());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsInt_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => value.AsInt());
        }

        [Theory]
        [InlineData("2147483648")]
        [InlineData("-2147483649")]
        public void ReadStringValueAsInt_ShouldThrowOverflowException(string? value)
        {
            Assert.Throws<OverflowException>(() => value.AsInt());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsNullableInt_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableInt());
        }
    }
}