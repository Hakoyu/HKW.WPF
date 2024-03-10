using System;
using System.Collections;
using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 空到布尔转换器
/// </summary>
public class NullToBoolConverter : InvertibleValueConverterBase<NullToBoolConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return value == null ^ IsInverted;
    }
}
