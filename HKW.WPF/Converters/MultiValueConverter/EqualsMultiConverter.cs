using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等转换器
/// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource RatioMarginConverter}">
///   <Binding Path="Value1" />
///   <Binding Path="Value2" />
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
public class EqualsMultiConverter : InvertibleMultiValueConverterBase<EqualsMultiConverter>
{
    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">参数必须为2</exception>
    public override object? Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (values.Length != 2)
            throw new NotImplementedException("Values length must be 2");
        return values[0]?.Equals(values[1]) ^ IsInverted;
    }
}
