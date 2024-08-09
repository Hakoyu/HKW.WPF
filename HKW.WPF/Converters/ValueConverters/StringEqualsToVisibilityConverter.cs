using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等字符串转换器
/// <para>示例:
/// <code><![CDATA[
/// Visibility={Binding Value, Converter={StaticResource StringEqualsToVisibilityConverter}, ConverterParameter="OK"}
/// Value == "OK" ? TrueValue : FalseValue
/// ]]></code></para>
/// </summary>
public class StringEqualsToVisibilityConverter
    : StringEqualsToValueBaseConverter<Visibility, StringEqualsToVisibilityConverter>
{
    /// <inheritdoc/>
    public StringEqualsToVisibilityConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
    }
}
