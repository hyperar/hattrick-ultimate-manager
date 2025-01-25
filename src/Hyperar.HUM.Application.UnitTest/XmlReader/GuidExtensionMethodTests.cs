namespace Hyperar.HUM.Application.UnitTest.XmlReader
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;

    public class GuidExtensionMethodTests
    {
        [Theory]
        [InlineData("1664225dd67a4ab69f26779c758fc111")]
        [InlineData("65ee0a9383844cf791fdb65018587501")]
        [InlineData("d850b865d4d24ed286320902ef8544b0")]
        [InlineData("7ab02cfbfe0c45578fb8c20fca146c3c")]
        [InlineData("e3c2d12e328b489a9ac265e1fd368f5d")]
        [InlineData("5d98d487-aea6-4778-900a-3d3f2b2cc981")]
        [InlineData("2266e443-e8fd-4e17-9edd-2acd7976f838")]
        [InlineData("52e1c5ba-bdf8-4d5e-aed3-3434fc2b86ee")]
        [InlineData("3de2782a-a833-4fd2-82e6-6e6ff2c40cf6")]
        [InlineData("0770a1ce-9f6d-4793-8730-68ddc7cba331")]
        [InlineData("{14d49dc4-2782-47ee-8cd4-c9fbeefd8214}")]
        [InlineData("{b857c001-bf38-4ce8-8de4-6aef682ca54a}")]
        [InlineData("{114577c5-e7d3-4bcf-99c3-cb86d6e5ba2b}")]
        [InlineData("{06874f13-6aaa-421e-a718-ae530783a92f}")]
        [InlineData("{a293f896-16b3-42e3-89a6-6719c8bd2ff0}")]
        [InlineData("{A1C6C991-FAD0-4B0C-8972-C0F539B1955B}")]
        [InlineData("{53778C70-4E3E-467B-B014-E668E5059426}")]
        [InlineData("{81D41239-6348-46AF-8DEA-7E531C4BAB19}")]
        [InlineData("{FB6E01E7-ACA9-476A-9FD9-3D0EBBCC8FFE}")]
        [InlineData("{75A5CB7B-7A67-4D45-ACCD-83E3751A89A4}")]
        public void ReadStringValueAsGuid_ShouldReturnGuid(string? value)
        {
            Assert.IsType<Guid>(value.AsGuid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ReadStringValueAsGuid_ShouldThrowArgumentException(string? value)
        {
            Assert.Throws<ArgumentException>(() => value.AsGuid());
        }

        [Theory]
        [InlineData(null)]
        public void ReadStringValueAsGuid_ShouldThrowArgumentNullException(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => value.AsGuid());
        }

        [Theory]
        [InlineData("48g4161c7b7c45b3bebc011dd089dc4f")]
        [InlineData("114g62ee39ef4db3ac7a0dcd52f78198")]
        [InlineData("4bd6810f7a9e4b4ba038g0c96843d269")]
        [InlineData("905c227f-d89g-4b9a-ace8-42e9d38f7aec")]
        [InlineData("z34a22cd-1204-42fc-881d-0dd72c3a3dc9")]
        [InlineData("10577e0f-7c52-474e-b6d9-122362f468v5")]
        [InlineData("{1z39653a-9892-40e2-848e-963ac94a29b0}")]
        [InlineData("{4ce56ee1-594c-44e9-b4c5-34ad4o64a903}")]
        [InlineData("{59a416c9-eb55-4zbe-85f9-9e881d2c6d66}")]
        [InlineData("{990542B8-4D82-428A-ACFD-EE3BE4O40D1E}")]
        [InlineData("{094C0ABF-456D-46CA-9A81-F742BT7C1E9F}")]
        [InlineData("{91860980-C2D3-4FD3-8F3F-63C92ID6E02E}")]
        public void ReadStringValueAsGuid_ShouldThrowFormatException(string? value)
        {
            Assert.Throws<FormatException>(() => value.AsGuid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ReadStringValueAsNullableGuid_ShouldBeNull(string? value)
        {
            Assert.Null(value.AsNullableGuid());
        }
    }
}