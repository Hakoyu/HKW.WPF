using System.Collections.Frozen;
using System.Globalization;
using System.Numerics;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.NumberCompareConverter"/>
public class NumberCompareConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public NumberCompareConverter()
    {
        CommonValueConverter = new CommonValueConverters.NumberCompareConverter();
    }
}

/// <inheritdoc cref="CommonValueConverters.NumberCompareByConverter"/>
public class NumberCompareByConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public NumberCompareByConverter()
    {
        CommonValueConverter = new CommonValueConverters.NumberCompareByConverter();
    }
}
