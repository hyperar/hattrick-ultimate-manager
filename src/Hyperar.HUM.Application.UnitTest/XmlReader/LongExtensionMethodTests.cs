namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class LongExtensionMethodTests
    {
        [Theory]
        [InlineData("-8124151394368380000")]
        [InlineData("-8004446673740950000")]
        [InlineData("-5772206447969780000")]
        [InlineData("-5426740095535660000")]
        [InlineData("-5119304330892240000")]
        [InlineData("-4710217571170190000")]
        [InlineData("-3238420447325040000")]
        [InlineData("-2813193156121430000")]
        [InlineData("-2704873217123950000")]
        [InlineData("-2596865350932030000")]
        [InlineData("-1637774856780310000")]
        [InlineData("2551852404936070000")]
        [InlineData("2660446120049130000")]
        [InlineData("4464955237312330000")]
        [InlineData("7392962267623790000")]
        [InlineData("7428646848117170000")]
        [InlineData("7724501183359450000")]
        [InlineData("8385686956684440000")]
        [InlineData("8601585695678780000")]
        [InlineData("8756817717155670000")]
        public void ReadStringValueAsLong_ShouldBeLong(string value)
        {
            Assert.IsType<long>(value.AsLong());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsLong_ShouldThrowArgumentException(string? value)
        {
            Assert.Throws<ArgumentException>(() => value.AsLong());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsLong_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => value.AsLong());
        }

        [Theory]
        [InlineData("9223372036854775808")]
        [InlineData("-9223372036854775809")]
        public void ReadStringValueAsLong_ShouldThrowOverflowException(string? value)
        {
            Assert.Throws<OverflowException>(() => value.AsLong());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ReadStringValueAsNullableLong_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableLong());
        }
    }
}