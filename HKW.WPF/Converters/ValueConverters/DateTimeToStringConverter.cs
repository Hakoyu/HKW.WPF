using System;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.DateTimeOffsetToStringConverter"/>
public class DateTimeOffsetToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public DateTimeOffsetToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.DateTimeOffsetToStringConverter()
        {
            GetFormat = () => Format,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> FormatProperty =
        CommonDependencyProperty.Register<DateTimeOffsetToStringConverter, string>(nameof(Format));

    /// <summary>
    /// 运算符类型
    /// </summary>
    public string Format
    {
        get => GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }
}

/// <inheritdoc cref="CommonValueConverters.DateTimeToStringConverter"/>
public class DateTimeToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public DateTimeToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.DateTimeToStringConverter()
        {
            GetFormat = () => Format,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> FormatProperty =
        CommonDependencyProperty.Register<DateTimeToStringConverter, string>(nameof(Format));

    /// <summary>
    /// 运算符类型
    /// </summary>
    public string Format
    {
        get => GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }
}
