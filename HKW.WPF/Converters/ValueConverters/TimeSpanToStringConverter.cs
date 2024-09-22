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
    /// <summary>
    /// 默认格式化
    /// </summary>
    protected const string DefaultFormat = "g";

    /// <summary>
    /// 默认最小值
    /// </summary>
    protected const string DefaultMinValueString = "";

    /// <inheritdoc/>
    public TimeSpanToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.TimeSpanToStringConverter()
        {
            GetMinValueString = () => MinValueString,
            GetFormat = () => Format,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> FormatProperty =
        CommonDependencyProperty.Register<TimeSpanToStringConverter, string>(
            nameof(Format),
            DefaultFormat
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

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> MinValueStringProperty =
        CommonDependencyProperty.Register<TimeSpanToStringConverter, string>(
            nameof(MinValueString),
            DefaultMinValueString
        );

    /// <summary>
    /// 最小值
    /// </summary>
    public string MinValueString
    {
        get => GetValue(MinValueStringProperty);
        set => SetValue(MinValueStringProperty, value);
    }
}
