using System.Globalization;
using System.Numerics;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.CalculatorMultiConverter"/>
public class CalculatorMultiConverter : MultiValueConverterBase
{
    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<NumberType> NumberTypeProperty =
        CommonDependencyProperty.Register<CalculatorMultiConverter, NumberType>(
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

    /// <inheritdoc/>
    public CalculatorMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.CalculatorMultiConverter()
        {
            GetNumberType = () => NumberType,
        };
    }
}
