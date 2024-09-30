using System;
using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串是null或空或空白转换器
/// </summary>
public class StringIsNullOrEmptyOrWhiteSpaceConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public StringIsNullOrEmptyOrWhiteSpaceConverter()
    {
        CommonValueConverter = new CommonValueConverters.StringIsNullOrEmptyOrWhiteSpaceConverter()
        {
            GetStringCheckType = () => StringCheckType
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<StringCheckType> StringCheckTypeProperty =
        CommonDependencyProperty.Register<
            StringIsNullOrEmptyOrWhiteSpaceConverter,
            StringCheckType
        >(nameof(StringCheckType));

    /// <summary>
    /// 为真时的值
    /// </summary>
    public StringCheckType StringCheckType
    {
        get => GetValue(StringCheckTypeProperty);
        set => SetValue(StringCheckTypeProperty, value);
    }
}
