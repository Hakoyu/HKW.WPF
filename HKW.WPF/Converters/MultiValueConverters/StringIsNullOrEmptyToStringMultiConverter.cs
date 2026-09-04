using System.Globalization;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.FirstStringStateToOtherStringMultiConverter"/>
public class FirstStringStateToOtherStringMultiConverter : MultiValueConverterBase
{
    /// <inheritdoc/>
    public FirstStringStateToOtherStringMultiConverter()
    {
        CommonValueConverter =
            new CommonValueConverters.FirstStringStateToOtherStringMultiConverter();
    }
}
