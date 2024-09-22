using System;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// Guid到字符串转换器
/// </summary>
public class GuidToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public GuidToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.GuidToStringConverter()
        {
            GetToUpper = () => ToUpper,
            GetFormat = () => Format,
        };
    }

    /// <summary>
    /// 默认格式化
    /// </summary>
    protected const string DefaultFormat = "D";

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> ToUpperProperty =
        CommonDependencyProperty.Register<GuidToStringConverter, bool>(nameof(ToUpper));

    /// <summary>
    /// 转换为大写
    /// </summary>
    public bool ToUpper
    {
        get => GetValue(ToUpperProperty);
        set => SetValue(ToUpperProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> FormatProperty =
        CommonDependencyProperty.Register<GuidToStringConverter, string>(
            nameof(Format),
            DefaultFormat
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
