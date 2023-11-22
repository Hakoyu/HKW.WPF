﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace VPet.ModMaker.Converters;

/// <summary>
/// 媒体颜色至画笔颜色转换器
/// </summary>
public class MediaColorToBrushConverter : IValueConverter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Color color)
            throw new ArgumentException("Not media color", nameof(value));
        return new SolidColorBrush(color);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush brush)
            throw new ArgumentException("Not SolidColorBrush", nameof(value));
        return brush.Color;
    }
}
