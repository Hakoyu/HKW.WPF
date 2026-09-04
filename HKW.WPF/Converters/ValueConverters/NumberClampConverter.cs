using System.Numerics;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.NumberClampConverter"/>
public class NumberClampConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public NumberClampConverter()
    {
        CommonValueConverter = new CommonValueConverters.NumberClampConverter()
        {
            GetNumberType = () => NumberType,
        };
    }

    /// <summary>
    ///
    /// </summary>
    public static readonly CommonDependencyProperty<NumberType> NumberTypeProperty =
        CommonDependencyProperty.Register<NumberClampConverter, NumberType>(nameof(NumberType));

    /// <summary>
    /// 格式化
    /// </summary>
    public NumberType NumberType
    {
        get => GetValue(NumberTypeProperty);
        set => SetValue(NumberTypeProperty, value);
    }
}
