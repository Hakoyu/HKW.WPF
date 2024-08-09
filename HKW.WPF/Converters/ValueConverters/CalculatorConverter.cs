using System.Globalization;
using System.Windows;
using HKW.HKWUtils.Extensions;
using HKW.HKWUtils.Utils;

namespace HKW.WPF.Converters;

/// <summary>
/// 计算器转换器
/// <para>示例:
/// <code><![CDATA[
/// <Binding Number,Converter="{StaticResource CalculatorConverter}",ConverterParameter="8"/>
/// return: Number + 8
/// ]]></code></para>
/// </summary>
public class CalculatorConverter : ValueConverterBase<CalculatorConverter>
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty NumberTypeProperty = DependencyProperty.Register(
        nameof(NumberType),
        typeof(NumberType),
        typeof(CalculatorConverter),
        new(NumberType.Double)
    );

    /// <summary>
    /// 数值类型
    /// </summary>
    public NumberType NumberType
    {
        get => (NumberType)GetValue(NumberTypeProperty);
        set => SetValue(NumberTypeProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty OperatorTypeProperty = DependencyProperty.Register(
        nameof(OperatorType),
        typeof(ArithmeticOperatorType),
        typeof(CalculatorConverter),
        new(ArithmeticOperatorType.Addition)
    );

    /// <summary>
    /// 运算符类型
    /// </summary>
    public ArithmeticOperatorType OperatorType
    {
        get => (ArithmeticOperatorType)GetValue(OperatorTypeProperty);
        set => SetValue(OperatorTypeProperty, value);
    }

    /// <inheritdoc/>
    public override object? Convert(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value == UnsetValue || parameter == UnsetValue)
            return 0;
        return NumberUtils.Arithmetic(value, parameter, NumberType, OperatorType);
    }

    /// <inheritdoc/>
    public override object? ConvertBack(
        object? value,
        Type? targetType,
        object? parameter,
        CultureInfo? culture
    )
    {
        if (value == UnsetValue || parameter == UnsetValue)
            return 0;
        return NumberUtils.Arithmetic(value, parameter, NumberType, OperatorType);
    }
}
