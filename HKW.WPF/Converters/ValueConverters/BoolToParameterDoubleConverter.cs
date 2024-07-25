using System.Globalization;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到布尔参数转换器
/// /// <para>示例:
/// <code><![CDATA[
/// <Binding Bool, Converter="{StaticResource BoolToParameterDoubleConverter}" ConverterParameter="1,2"/>
/// return: Bool ? 1 : 2
/// ]]></code></para>
/// </summary>
public class BoolToParameterDoubleConverter
    : BoolToParameterConverterBase<BoolToParameterDoubleConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (parameter is not string str || string.IsNullOrWhiteSpace(str))
            return 0.0;
        var r = ConverterUtils.GetBool(value);
        var spilt = str.AsSpan().Split(Separator);
        spilt.MoveNext();
        if (r)
            return double.Parse(spilt.Current);
        else if (spilt.MoveNext())
            return double.Parse(spilt.Current);
        return 0.0;
    }
}
