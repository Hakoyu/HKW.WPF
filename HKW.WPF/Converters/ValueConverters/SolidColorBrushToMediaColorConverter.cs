using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HKW.WPF.Converters;

/// <summary>
/// 笔刷到媒体颜色转换器
/// </summary>
public class SolidColorBrushToMediaColorConverter : ValueConverterBase
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
