using System;
using System.Diagnostics;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.DebugConverter"/>
public class DebugConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public DebugConverter()
    {
        CommonValueConverter = new CommonValueConverters.DebugConverter();
    }
}
