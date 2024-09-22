using System.Globalization;
using System.Numerics;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到布尔参数转换器
/// /// <para>示例:
/// <code><![CDATA[
/// <Binding Bool, Converter="{StaticResource BoolToParameterDoubleConverter}" ConverterParameter="1,2"/>
/// result: Bool ? 1 : 2
/// ]]></code></para>
/// </summary>
public class BoolToParameterNumberConverter<T> : BoolToSplitParameterConverterBase
    where T : struct, INumber<T>
{
    /// <inheritdoc/>
    public BoolToParameterNumberConverter()
    {
        CommonValueConverter = new CommonValueConverters.BoolToParameterNumberConverter<T>();
    }
}
