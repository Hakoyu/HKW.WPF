using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.ValueToEnumerableConverter"/>
public class ValueToEnumerableConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public ValueToEnumerableConverter()
    {
        CommonValueConverter = new CommonValueConverters.ValueToEnumerableConverter();
    }
}
