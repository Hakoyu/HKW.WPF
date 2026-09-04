using System.Globalization;
using System.Numerics;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.CalculatorConverter"/>
public class CalculatorConverter : ValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<NumberType> NumberTypeProperty =
        CommonDependencyProperty.Register<CalculatorConverter, NumberType>(
            nameof(NumberType),
            CommonValueConverters.CalculatorConverter.DefaultNumberType
        );

    /// <summary>
    /// 数值类型
    /// </summary>
    public NumberType NumberType
    {
        get => GetValue(NumberTypeProperty);
        set => SetValue(NumberTypeProperty, value);
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<ArithmeticOperatorType> OperatorTypeProperty =
        CommonDependencyProperty.Register<CalculatorConverter, ArithmeticOperatorType>(
            nameof(OperatorType),
            CommonValueConverters.CalculatorConverter.DefaultArithmeticOperatorType
        );

    /// <summary>
    /// 运算符类型
    /// </summary>
    public ArithmeticOperatorType OperatorType
    {
        get => GetValue(OperatorTypeProperty);
        set => SetValue(OperatorTypeProperty, value);
    }

    /// <inheritdoc/>
    public CalculatorConverter()
    {
        CommonValueConverter = new CommonValueConverters.CalculatorConverter()
        {
            GetNumberType = () => NumberType,
            GetOperatorType = () => OperatorType,
        };
    }
}
