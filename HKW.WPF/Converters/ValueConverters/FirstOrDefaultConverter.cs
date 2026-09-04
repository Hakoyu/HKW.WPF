using System;
using System.Collections;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.FirstOrDefaultConverter"/>
public class FirstOrDefaultConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public FirstOrDefaultConverter()
    {
        CommonValueConverter = new CommonValueConverters.FirstOrDefaultConverter();
    }
}
