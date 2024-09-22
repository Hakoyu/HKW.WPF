using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等字符串转换器
/// <para>示例:
/// <code><![CDATA[
/// IsChecked={Binding Value, Converter={StaticResource EqualsToVisibilityConverter}, ConverterParameter={x:Null}}
/// ]]></code></para>
/// </summary>
public class EqualsToVisibilityConverter : EqualsToValueConverter<Visibility>
{
    /// <inheritdoc/>
    public EqualsToVisibilityConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
    }
}
