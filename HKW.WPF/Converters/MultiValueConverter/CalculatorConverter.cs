﻿using System.Globalization;
using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 计算器转换器
/// <para>示例:
/// <code><![CDATA[
/// <MultiBinding Converter="{StaticResource CalculatorConverter}">
///   <Binding Path="Num1" />
///   <Binding Source="+" />
///   <Binding Path="Num2" />
///   <Binding Source="-" />
///   <Binding Path="Num3" />
///   <Binding Source="*" />
///   <Binding Path="Num4" />
///   <Binding Source="/" />
///   <Binding Path="Num5" />
/// </MultiBinding>
/// //
/// <MultiBinding Converter="{StaticResource CalculatorConverter}" ConverterParameter="+-*/">
///   <Binding Path="Num1" />
///   <Binding Path="Num2" />
///   <Binding Path="Num3" />
///   <Binding Path="Num4" />
///   <Binding Path="Num5" />
/// </MultiBinding>
/// ]]></code></para>
/// </summary>
/// <exception cref="Exception">绑定的数量不正确</exception>
public class CalculatorConverter : MultiValueConverterBase<CalculatorConverter>
{
    /// <inheritdoc/>
    public override object? Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture
    )
    {
        if (values.Any(i => i == UnsetValue))
            return 0.0;
        if (values.Length == 1)
            return values[0];
        double result = System.Convert.ToDouble(values[0]);
        if (parameter is string operators && string.IsNullOrWhiteSpace(operators) is false)
        {
            if (operators.Length != values.Length - 1)
                throw new Exception("Parameter error: operator must be one more than parameter");
            for (int i = 1; i < values.Length - 1; i++)
                result = Operation(result, operators[i - 1], System.Convert.ToDouble(values[i]));
            result = Operation(result, operators.Last(), System.Convert.ToDouble(values.Last()));
        }
        else
        {
            if (System.Convert.ToBoolean(values.Length & 1) is false)
                throw new Exception("Parameter error: Incorrect quantity");
            bool isNumber = false;
            char currentOperator = '0';
            for (int i = 1; i < values.Length - 1; i++)
            {
                if (isNumber is false)
                {
                    currentOperator = ((string)values[i]!)[0];
                    isNumber = true;
                }
                else
                {
                    var value = System.Convert.ToDouble(values[i]);
                    result = Operation(result, currentOperator, value);
                    isNumber = false;
                }
            }
            result = Operation(result, currentOperator, System.Convert.ToDouble(values.Last()));
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
