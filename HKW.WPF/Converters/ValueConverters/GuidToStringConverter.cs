using System;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.GuidToStringConverter"/>
public class GuidToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public GuidToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.GuidToStringConverter()
        {
            GetStringTo = () => StringTo,
            GetFormat = () => Format,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool?> StringToProperty =
        CommonDependencyProperty.Register<GuidToStringConverter, bool?>(nameof(StringTo));

    /// <summary>
    /// 转换为大写
    /// </summary>
    public bool? StringTo
    {
        get => GetValue(StringToProperty);
        set => SetValue(StringToProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> FormatProperty =
        CommonDependencyProperty.Register<GuidToStringConverter, string>(
            nameof(Format),
            CommonValueConverters.GuidToStringConverter.DefaultFormat
        );

    /// <summary>
    /// 格式化
    /// </summary>
    public string Format
    {
        get => GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }
}
