using System;
using System.Diagnostics;

namespace HKW.WPF.Converters;

/// <summary>
/// 调试转换器
/// </summary>
public class DebugConverter : ValueConverterBase<DebugConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        Debug.WriteLine(
            $"DebugConverter.Convert(value={value}, targetType={targetType}, parameter={parameter}, culture={culture})"
        );

        return value;
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        Debug.WriteLine(
            $"DebugConverter.ConvertBack(value={value}, targetType={targetType}, parameter={parameter}, culture={culture})"
        );

        return value;
    }
}
