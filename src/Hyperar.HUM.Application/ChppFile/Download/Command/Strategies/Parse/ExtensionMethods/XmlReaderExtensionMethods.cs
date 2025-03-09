namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    internal static class XmlReaderMethods
    {
        internal static bool AsBool(this string? value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return value switch
            {
                "0" => false,
                "1" => true,
                _ => bool.Parse(value)
            };
        }

        internal static DateTime AsDateTime(this string? value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        internal static decimal AsDecimal(this string? value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return decimal.Parse(
                NormalizeDecimalValue(
                    value));
        }

        internal static Guid AsGuid(this string? value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return Guid.Parse(value);
        }

        internal static int AsInt(this string? value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return int.Parse(value);
        }

        internal static long AsLong(this string? value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return long.Parse(value);
        }

        internal static bool? AsNullableBool(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : AsBool(value);
        }

        internal static DateTime? AsNullableDateTime(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : AsDateTime(value);
        }

        internal static decimal? AsNullableDecimal(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : AsDecimal(value);
        }

        internal static Guid? AsNullableGuid(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : AsGuid(value);
        }

        internal static int? AsNullableInt(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : AsInt(value);
        }

        internal static long? AsNullableLong(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : AsLong(value);
        }

        internal static string? AsNullableString(this string? value)
        {
            return value;
        }

        internal static string AsString(this string? value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return value!;
        }

        internal static bool CheckNode(this XmlReader reader, params string[] expectedNames)
        {
            return Array.Exists(expectedNames, x => x.Equals(reader.Name, StringComparison.OrdinalIgnoreCase));
        }

        internal static async Task<string?> ReadValueAsync(this XmlReader xmlReader)
        {
            return await xmlReader.ReadElementContentAsStringAsync();
        }

        private static string NormalizeDecimalValue(string value)
        {
            var currentCultureDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.First();

            if (!value.Any(x => (x == ',' || x == '.') && x != currentCultureDecimalSeparator))
            {
                return value;
            }

            var actualSeparator = value.Where(x => x is ',' or '.')
                .Single(x => !char.IsDigit(x));

            return value.Replace(actualSeparator, currentCultureDecimalSeparator);
        }
    }
}