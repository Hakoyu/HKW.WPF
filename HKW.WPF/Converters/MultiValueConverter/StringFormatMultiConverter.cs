using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串格式化器
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
            return string.Format(format, values);
        }
        else
        {
            format = (string)values[0]!;
            return string.Format(format, values.Skip(1).ToArray());
        }
    }
}
