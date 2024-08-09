using System.Globalization;
using System.Windows;

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
public class StringFormatMultiConverter : MultiValueConverterBase<StringFormatMultiConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty HiddenUnsetAndNullProperty =
        DependencyProperty.Register(
            nameof(HiddenUnsetAndNull),
            typeof(bool),
            typeof(StringFormatMultiConverter)
        );

    /// <summary>
    /// 隐藏未设置和空占位符
    /// </summary>
    public bool HiddenUnsetAndNull
    {
        get => (bool)GetValue(HiddenUnsetAndNullProperty);
        set => SetValue(HiddenUnsetAndNullProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object?[] values,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (parameter is string format && string.IsNullOrWhiteSpace(format) is false)
        {
            if (HiddenUnsetAndNull)
                return string.Format(
                    format,
                    values.Select(v => v is null || v == UnsetValue ? string.Empty : v).ToArray()
                );
            else
                return string.Format(format, values);
        }
        else
        {
            format = (string)values[0]!;
            var temp = values.Skip(1);
            if (HiddenUnsetAndNull)
                return string.Format(
                    format,
                    temp.Select(v => v is null || v == UnsetValue ? string.Empty : v).ToArray()
                );
            else
                return string.Format(format, temp.ToArray());
        }
    }
}
