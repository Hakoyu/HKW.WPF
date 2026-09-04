using System.Globalization;
using System.Numerics;
using System.Windows;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.CalculatorMultiConverter"/>
public class CalculatorMultiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public CalculatorMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.CalculatorMultiConverter();
    }
}
