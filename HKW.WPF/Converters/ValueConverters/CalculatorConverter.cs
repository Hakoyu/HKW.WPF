using System.Globalization;
using System.Numerics;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <summary>
/// 计算器转换器
/// <para>示例:
/// <code><![CDATA[
/// <Binding Number,Converter="{StaticResource CalculatorConverter}",ConverterParameter="8"/>
/// return: Number + 8
/// ]]></code></para>
/// </summary>
public class CalculatorConverter : ValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<NumberType> NumberTypeProperty =
        CommonDependencyProperty.Register<CalculatorConverter, NumberType>(
            nameof(NumberType),
            NumberType.Double
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
            ArithmeticOperatorType.Addition
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
    public override void CommonValueConverterInitialize(
        CommonValueConverters.ValueConverterBase commonValueConverter
    )
    {
        base.CommonValueConverterInitialize(commonValueConverter);
        if (commonValueConverter is not CommonValueConverters.CalculatorConverter converter)
            return;
        converter.GetNumberType = () => NumberType;
        converter.GetOperatorType = () => OperatorType;
    }
}
