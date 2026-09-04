using System.Globalization;
using HKW.HKWUtils.Extensions;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.FirstBoolToValueMultiConverter"/>
public class FirstBoolToValueMultiConverter : InvertibleMultiValueConverterBase
{
    /// <inheritdoc/>
    public FirstBoolToValueMultiConverter()
    {
        CommonValueConverter = new CommonValueConverters.FirstBoolToValueMultiConverter();
    }
}
