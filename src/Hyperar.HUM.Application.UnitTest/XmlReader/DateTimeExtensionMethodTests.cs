namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class DateTimeExtensionMethodTests
    {
        [Theory]
        [InlineData("2025-01-12", 2025, 01, 12)]
        [InlineData("0001-01-01 00:00:00", 1, 1, 1, 0, 0, 0)]
        [InlineData("1987-05-17 8:22:37", 1987, 05, 17, 8, 22, 37)]
        [InlineData("2025-01-05 5:7:12", 2025, 01, 05, 5, 7, 12)]
        [InlineData("2085-04-22 19:55:05", 2085, 04, 22, 19, 55, 05)]
        [InlineData("9999-12-31 23:59:59", 9999, 12, 31, 23, 59, 59)]
        public void ReadStringValueAsDateTime_ShouldBeEqual(
            string? value,
            int year,
            int month,
            int day,
            int hour = 0,
            int minute = 0,
            int second = 0)
        {
            Assert.Equal(value.AsDateTime(), new DateTime(year, month, day, hour, minute, second));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsDateTime_ShouldThrowArgumentException(string? value)
        {
            Assert.Throws<ArgumentException>(() => value.AsDateTime());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsDateTime_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => value.AsDateTime());
        }

        [Theory]
        [InlineData("0000-00-00 00:00:00")]
        [InlineData("10000-01-01 00:00:00")]
        [InlineData("2025-29-02 00:00:00")]
        [InlineData("2025-04-31 00:00:00")]
        public void ReadStringValueAsDateTime_ShouldThrowFormatException(string? value)
        {
            Assert.Throws<FormatException>(() => value.AsDateTime());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsNullableDateTime_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableDateTime());
        }
    }
}