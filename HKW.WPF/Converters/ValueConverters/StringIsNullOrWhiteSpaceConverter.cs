using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串是null或者空白转换器
/// </summary>
public class StringIsNullOrWhiteSpaceConverter
    : InvertibleValueConverterBase<StringIsNullOrWhiteSpaceConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        return string.IsNullOrWhiteSpace(value as string) ^ IsInverted;
    }
}
