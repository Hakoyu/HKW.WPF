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
}
