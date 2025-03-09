namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class DecimalExtensionMethodTests
    {
        [Theory]
        [InlineData("0,001", 0.001d)]
        [InlineData("0,0025", 0.0025d)]
        [InlineData("0,005", 0.005d)]
        [InlineData("0,01", 0.01d)]
        [InlineData("0,02", 0.02d)]
        [InlineData("0,05", 0.05d)]
        [InlineData("0,1", 0.1d)]
        [InlineData("0,2", 0.2d)]
        [InlineData("0,25", 0.25d)]
        [InlineData("0,4", 0.4d)]
        [InlineData("0,5", 0.5d)]
        [InlineData("1", 1d)]
        [InlineData("1,25", 1.25d)]
        [InlineData("2", 2d)]
        [InlineData("2,5", 2.5d)]
        [InlineData("4", 4d)]
        [InlineData("5", 5d)]
        [InlineData("8", 8d)]
        [InlineData("10", 10d)]
        [InlineData("15", 15d)]
        [InlineData("20", 20d)]
        [InlineData("25", 25d)]
        [InlineData("50", 50d)]
        [InlineData("1000", 1000d)]
        [InlineData("0.001", 0.001d)]
        [InlineData("0.0025", 0.0025d)]
        [InlineData("0.005", 0.005d)]
        [InlineData("0.01", 0.01d)]
        [InlineData("0.02", 0.02d)]
        [InlineData("0.05", 0.05d)]
        [InlineData("0.1", 0.1d)]
        [InlineData("0.2", 0.2d)]
        [InlineData("0.25", 0.25d)]
        [InlineData("0.4", 0.4d)]
        [InlineData("0.5", 0.5d)]
        [InlineData("1.25", 1.25d)]
        [InlineData("2.5", 2.5d)]
        public void ReadStringValueAsDecimal_ShouldBeEqual(string? value, decimal compareValue)
        {
            Assert.Equal(value.AsDecimal(), compareValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsDecimal_ShouldThrowArgumentException(string? value)
        {
            Assert.Throws<ArgumentException>(() => value.AsDecimal());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsDecimal_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => value.AsDecimal());
        }

        [Theory]
        [InlineData("a")]
        [InlineData("0 001")]
        [InlineData("0 0025")]
        [InlineData("0 005")]
        [InlineData("0 01")]
        [InlineData("0 02")]
        [InlineData("0 05")]
        [InlineData("0 1")]
        [InlineData("0 2")]
        [InlineData("0 25")]
        [InlineData("0 4")]
        [InlineData("0 5")]
        [InlineData("1 25")]
        [InlineData("2 5")]
        public void ReadStringValueAsDecimal_ShouldThrowFormatException(string? value)
        {
            Assert.Throws<FormatException>(() => value.AsDecimal());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsNullableDecimal_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableDecimal());
        }
    }
}