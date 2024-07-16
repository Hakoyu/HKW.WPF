using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等转换器
/// <para>示例:
/// <code><![CDATA[
/// IsChecked={Binding Value, Converter={StaticResource EqualsConverter}, ConverterParameter={x:Null}}
/// ]]></code></para>
/// </summary>
public class EqualsConverter : InvertibleValueConverterBase<EqualsConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        return value?.Equals(parameter) is true ^ IsInverted;
    }
}

/// <summary>
/// 相等字符串转换器
/// <para>示例:
/// <code><![CDATA[
/// IsChecked={Binding Value, Converter={StaticResource EqualsConverter}, ConverterParameter={x:Null}}
/// ]]></code></para>
/// </summary>
public class EqualsStringConverter : InvertibleValueConverterBase<EqualsStringConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value is null || parameter is null)
            return IsInverted;
        return value.ToString() == parameter.ToString() ^ IsInverted;
    }
}
