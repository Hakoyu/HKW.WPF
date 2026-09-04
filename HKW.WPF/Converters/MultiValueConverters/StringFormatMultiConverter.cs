using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.StringFormatMultiConverter"/>
public class StringFormatMultiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public StringFormatMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.StringFormatMultiConverter()
        {
            GetReplaceUnsetValue = () => ReplaceUnsetValue,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<string> ReplaceUnsetValueProperty =
        CommonDependencyProperty.Register<StringFormatMultiConverter, string>(
            nameof(ReplaceUnsetValue)
        );

    /// <summary>
    /// 隐藏未设置和空占位符
    /// </summary>
    public string ReplaceUnsetValue
    {
        get => GetValue(ReplaceUnsetValueProperty);
        set => SetValue(ReplaceUnsetValueProperty, value);
    }
}
