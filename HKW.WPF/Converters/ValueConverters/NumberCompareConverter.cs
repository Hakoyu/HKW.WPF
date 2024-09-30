using System.Collections.Frozen;
using System.Globalization;
using System.Numerics;

namespace HKW.WPF.Converters;

/// <summary>
/// 数字比较转换器
/// <para>示例:
/// <code><![CDATA[
/// IsEnabled={Binding Numer, Converter={StaticResource NumberCompareConverter}, ConverterParameter=">0"}
/// Numer > 0 ? true : false
/// ]]></code></para>
/// </summary>
public class NumberCompareConverter<T> : InvertibleValueConverterBase
    where T : struct, INumber<T>
{
    /// <inheritdoc/>
    public NumberCompareConverter()
    {
        CommonValueConverter = new CommonValueConverters.NumberCompareConverter<T>();
    }
}

/// <summary>
/// double比较转换器
/// </summary>
public class DoubleCompareConverter : NumberCompareConverter<double> { }
