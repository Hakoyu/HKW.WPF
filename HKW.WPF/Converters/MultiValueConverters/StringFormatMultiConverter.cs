using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串格式化转换器
/// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource MarginConverter}">
///   <Binding Path="StringFormat" />
///   <Binding Path="Value1" />
///   <Binding Path="Value2" />
/// </MultiBinding>
/// OR
/// <MultiBinding Converter="{StaticResource MarginConverter}" ConverterParameter="{}{0}{1}">
///   <Binding Path="Value1" />
///   <Binding Path="Value2" />
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
public class StringFormatMultiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public StringFormatMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.StringFormatMultiConverter();
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<bool> HiddenUnsetAndNullProperty =
        CommonDependencyProperty.Register<StringFormatMultiConverter, bool>(
            nameof(HiddenUnsetAndNull)
        );

    /// <summary>
    /// 隐藏未设置和空占位符
    /// </summary>
    public bool HiddenUnsetAndNull
    {
        get => GetValue(HiddenUnsetAndNullProperty);
        set => SetValue(HiddenUnsetAndNullProperty, value);
    }
}
