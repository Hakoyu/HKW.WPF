using System.Collections.Frozen;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 相等字符串转换器
/// <para>示例:
/// <code><![CDATA[
/// IsEnabled={Binding Numer, Converter={StaticResource NumberCompareConverter}, ConverterParameter=">0"}
/// Numer > 0 ? true : false
/// ]]></code></para>
/// </summary>
public class NumberCompareConverter : InvertibleValueConverterBase<NumberCompareConverter>
{
    internal static FrozenSet<string> EqualsType { get; } =
        FrozenSet.ToFrozenSet<string>([">", "<", "==", "!=", "<=", ">="]);

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (parameter is not string str)
            return 0.0;
        var compare = EqualsType.First(str.StartsWith);
        if (compare is null)
            return 0.0;
        var number = System.Convert.ToDouble(value);
        var target = System.Convert.ToDouble(str[compare.Length..]);
        return EqualsAciton(number, compare, target) ^ IsInverted;
    }

    /// <summary>
    /// 比较行动
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="compare">比较</param>
    /// <param name="target">目标</param>
    /// <returns>结果</returns>
    public static bool EqualsAciton(double value, string compare, double target)
    {
        return compare switch
        {
            ">" => value > target,
            "<" => value < target,
            "==" => value == target,
            "!=" => value != target,
            "<=" => value <= target,
            ">=" => value >= target,
            _ => false
        };
    }
}
