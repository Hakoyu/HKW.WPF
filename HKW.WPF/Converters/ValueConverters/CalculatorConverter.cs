using System.Globalization;
using System.Windows;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <summary>
/// 计算器转换器
/// <para>示例:
/// <code><![CDATA[
/// <Binding Number,Converter="{StaticResource CalculatorConverter}" ConverterParameter="+8"/>
/// return: Number + 8
/// ]]></code></para>
/// </summary>
/// <exception cref="Exception">绑定的数量不正确</exception>
public class CalculatorConverter : ValueConverterBase<CalculatorConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty SeparatorProperty = DependencyProperty.Register(
        nameof(Separator),
        typeof(char),
        typeof(CalculatorConverter),
        new(',')
    );

    /// <summary>
    /// 分割符
    /// </summary>
    public char Separator
    {
        get => (char)GetValue(SeparatorProperty);
        set => SetValue(SeparatorProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value == UnsetValue)
            return 0.0;
        var result = System.Convert.ToDouble(value);
        if (parameter is string data && string.IsNullOrWhiteSpace(data) is false)
        {
            var split = data.AsSpan().Split(Separator);
            split.MoveNext();
            var currentOperator = split.Current[0];
            split.MoveNext();
            result = Operation(
                result,
                currentOperator,
                System.Convert.ToDouble(split.Current.ToString())
            );
            return result;
        }
        return result;
    }

    /// <summary>
    /// 计算
    /// </summary>
    /// <param name="value1">值1</param>
    /// <param name="operatorChar">符号</param>
    /// <param name="value2">值2</param>
    /// <returns>结果</returns>
    /// <exception cref="NotImplementedException">不支持的符号</exception>
    public static double Operation(double value1, char operatorChar, double value2)
    {
        return operatorChar switch
        {
            '+' => value1 + value2,
            '-' => value1 - value2,
            '*' => value1 * value2,
            '/' => value1 / value2,
            '%' => value1 % value2,
            _ => throw new NotImplementedException($"Unsupported operator '{operatorChar}'"),
        };
    }
}
