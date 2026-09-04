using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.EnumEqualsConverter"/>
public class EnumEqualsConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public EnumEqualsConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumEqualsConverter();
    }
}
