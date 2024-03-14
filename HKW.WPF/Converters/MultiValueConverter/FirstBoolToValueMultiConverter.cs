using System.Globalization;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <summary>
/// 第一个为布尔到值转换器
/// /// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource FirstBoolToValueConverter}">
///   <Binding Path="BoolValue" />
///   <Binding Path="TrueValue" />
///   <Binding Path="FalueValue" /> // default is null
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
public class FirstBoolToValueMultiConverter
    : InvertibleMultiValueConverterBase<FirstBoolToValueMultiConverter>
{
    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">参数数量必须为2或3</exception>
    public override object? Convert(
        object?[] values,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (values.Length != 2 && values.Length != 3)
            throw new ArgumentException("Values count must be 2 or 3");
        var first = values[0];
        if (first == UnsetValue)
            return null;
        var result = System.Convert.ToBoolean(first);
        return result ? values[1] : values.GetValueOrDefault(2);
    }
}
