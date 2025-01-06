namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Xml;

    internal static class XmlReaderMethods
    {
        private const string comma = ",";

        private const string period = ".";

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

            var numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (value.Contains(period, StringComparison.CurrentCulture) && numberDecimalSeparator != period)
            {
                return decimal.Parse(value.Replace(period, numberDecimalSeparator));
            }
            else
            {
                return value.Contains(comma, StringComparison.CurrentCulture) && numberDecimalSeparator != comma
                     ? decimal.Parse(value.Replace(comma, numberDecimalSeparator))
                     : decimal.Parse(value);
            }
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

            var decimalValue = value.Contains(period, StringComparison.CurrentCulture) && numberDecimalSeparator != period
                ? decimal.Parse(value.Replace(period, numberDecimalSeparator))
                : value.Contains(comma, StringComparison.CurrentCulture) && numberDecimalSeparator != comma
                             ? decimal.Parse(value.Replace(comma, numberDecimalSeparator))
                             : decimal.Parse(value);

            return nullValue.HasValue && nullValue.Value == decimalValue
                 ? null
                 : decimalValue;
        }

        internal static async Task<decimal?> ReadNullableDecimalAsync(
            this XmlReader reader,
            params string[]? expectedNames)
        {
            if (expectedNames != null && expectedNames.Length > 0 && !reader.CheckNode(expectedNames))
            {
                return null;
            }

            var value = await reader.ReadElementContentAsStringAsync();

            var numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else if (value.Contains(period, StringComparison.CurrentCulture) && numberDecimalSeparator != period)
            {
                return decimal.Parse(value.Replace(period, numberDecimalSeparator));
            }
            else
            {
                return value.Contains(comma, StringComparison.CurrentCulture) && numberDecimalSeparator != comma
                     ? decimal.Parse(value.Replace(comma, numberDecimalSeparator))
                     : decimal.Parse(value);
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
    }
}