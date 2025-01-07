﻿namespace Hyperar.HUM.UserInterface.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    internal class EnumToTranslatedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null
                 ? null
                 : Globalization.Enums.ResourceManager.GetString($"{value.GetType().Name}_{value}") ?? string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}