using System.Globalization;
using System.Windows.Media;

namespace HKW.WPF.Converters;

/// <summary>
/// 媒体颜色到笔刷转换器
/// </summary>
public class MediaColorToSolidColorBrushConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is not SolidColorBrush brush)
            throw new ArgumentException("Not SolidColorBrush", nameof(value));
        return brush.Color;
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is not Color color)
            throw new ArgumentException("Not media color", nameof(value));
        return new SolidColorBrush(color);
    }
}
