using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 字符串为Null或空到字符串转换器
/// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource MarginConverter}">
///   <Binding Path="Str1" />
///   <Binding Path="Str2" />
/// </MultiBinding>
/// result:
/// string.IsNullOrEmpty(Str1) ? Str2 : Str1
/// ]]></code></para>
/// </summary>
public class StringIsNullOrEmptyToStringMultiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public override object? Convert(
        object?[] values,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (values == null)
            return null;
        var str1 = values[0]?.ToString();
        var str2 = values[1]?.ToString();
        if (string.IsNullOrEmpty(str1))
            return str2;
        return str1;
    }
}
