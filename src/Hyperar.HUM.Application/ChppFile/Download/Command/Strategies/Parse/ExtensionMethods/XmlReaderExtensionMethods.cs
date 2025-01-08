namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Xml;

    internal static class XmlReaderMethods
    {
        internal static bool CheckNode(this XmlReader reader, params string[] expectedNames)
        {
            return Array.Exists(expectedNames, x => x.Equals(reader.Name, StringComparison.OrdinalIgnoreCase));
        }

        internal static async Task<bool> ReadBoolAsync(this XmlReader reader)
        {
            var value = await reader.ReadElementContentAsStringAsync();

            return value.Length == 1
                 ? value == "1"
                 : value.Equals(bool.TrueString, StringComparison.CurrentCultureIgnoreCase);
        }

        internal static async Task<DateTime> ReadDateTimeAsync(this XmlReader reader)
        {
            var value = await reader.ReadElementContentAsStringAsync();

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        internal static async Task<decimal> ReadDecimalAsync(this XmlReader reader)
        {
            var value = await reader.ReadElementContentAsStringAsync();

            return decimal.Parse(
                    NormalizeDecimalValue(
                        value));
        }

        internal static async Task<Guid> ReadGuidAsync(this XmlReader reader)
        {
            return Guid.Parse(await reader.ReadElementContentAsStringAsync());
        }

        internal static async Task<int> ReadIntAsync(this XmlReader reader)
        {
            return int.Parse(await reader.ReadElementContentAsStringAsync());
        }

        internal static async Task<long> ReadLongAsync(this XmlReader reader)
        {
            return long.Parse(await reader.ReadElementContentAsStringAsync());
        }

        internal static async Task<DateTime?> ReadNullableDateTimeAsync(
            this XmlReader reader,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value)
                 ? null
                 : DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        internal static async Task<decimal?> ReadNullableDecimalAsync(
            this XmlReader reader,
            decimal? nullValue,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            var numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            var decimalValue = decimal.Parse(
                    NormalizeDecimalValue(
                        value));

            return nullValue.HasValue && nullValue.Value == decimalValue
                 ? null
                 : decimalValue;
        }

        internal static async Task<decimal?> ReadNullableDecimalAsync(
            this XmlReader reader,
            params string[]? expectedNames)
        {
            expectedNames ??= Array.Empty<string>();

            if (expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            var numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else
            {
                return decimal.Parse(
                    NormalizeDecimalValue(
                        value));
            }
        }

        internal static async Task<int?> ReadNullableIntAsync(
            this XmlReader reader,
            int? nullValue,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            return value == nullValue?.ToString() || string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
        }

        internal static async Task<int?> ReadNullableIntAsync(
            this XmlReader reader,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
        }

        internal static async Task<long?> ReadNullableLongAsync(
            this XmlReader reader,
            long? nullValue,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            return value == nullValue?.ToString() ? null : long.Parse(value);
        }

        internal static async Task<long?> ReadNullableLongAsync(
            this XmlReader reader,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value) ? null : long.Parse(value);
        }

        internal static async Task<string?> ReadNullableStringAsync(
            this XmlReader reader,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        internal static async Task<string> ReadStringAsync(this XmlReader reader)
        {
            return await reader.ReadElementContentAsStringAsync();
        }

        private static string NormalizeDecimalValue(string value)
        {
            string actualSeparator = value.Single(x => !char.IsDigit(x))
                .ToString();

            return value.Replace(actualSeparator, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }
    }
}