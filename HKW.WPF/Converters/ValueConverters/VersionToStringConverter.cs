using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.VersionToStringConverter"/>
public class VersionToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public VersionToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.VersionToStringConverter();
    }
}
