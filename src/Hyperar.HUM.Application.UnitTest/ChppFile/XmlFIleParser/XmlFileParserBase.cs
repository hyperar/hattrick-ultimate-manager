namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFIleParser
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    public abstract class XmlFileParserBase
    {
        protected async Task<byte[]> OpenFile(string fileName)
        {
            var basePath = Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location);

            ArgumentException.ThrowIfNullOrWhiteSpace(basePath);

            var filePath = Path.Combine(basePath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            return await File.ReadAllBytesAsync(filePath);
        }
    }
}