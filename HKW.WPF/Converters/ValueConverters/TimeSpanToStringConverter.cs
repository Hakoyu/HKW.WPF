using System;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 时间范围到字符串转换器
/// </summary>
public class TimeSpanToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public TimeSpanToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.TimeSpanToStringConverter()
        {
            GetFormat = () => Format,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> FormatProperty =
        CommonDependencyProperty.Register<TimeSpanToStringConverter, string>(
            nameof(Format),
            CommonValueConverters.TimeSpanToStringConverter.DefaultFormat
        );

    /// <summary>
    /// 时间格式化
    /// <para>
    /// 时间格式化参考s: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-timespan-format-strings
    /// </para>
    /// </summary>
    public string Format
    {
        get => GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }
}
