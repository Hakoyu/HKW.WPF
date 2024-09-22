using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.StringCaseConverter"/>
public class StringCaseConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public StringCaseConverter()
    {
        CommonValueConverter = new CommonValueConverters.StringCaseConverter();
    }
}
