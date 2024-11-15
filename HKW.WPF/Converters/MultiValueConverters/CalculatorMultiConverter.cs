using System.Globalization;
using System.Numerics;
using System.Windows;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.CalculatorMultiConverter{T}"/>
public class CalculatorMultiConverter<T> : MultiValueConverterBase
    where T : struct, INumber<T>
{
    /// <inheritdoc/>
    public CalculatorMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.CalculatorMultiConverter<T>();
    }

    /// <inheritdoc/>
    public override void InitializeValueConverter(
        CommonValueConverters.MultiValueConverterBase commonValueConverter
    )
    {
        commonValueConverter.GetDefaultResult = () => T.Zero;
    }
}

/// <summary>
/// Double计算转换器
/// </summary>
public class DoubleCalculatorMultiConverter : CalculatorMultiConverter<double> { }
