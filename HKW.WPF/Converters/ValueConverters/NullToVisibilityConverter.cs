using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 空到可见度转换器
/// </summary>
public class NullToVisibilityConverter : InvertibleValueConverterBase<NullToVisibilityConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        return value is null ^ IsInverted ? Visibility.Visible : Visibility.Collapsed;
    }
}
