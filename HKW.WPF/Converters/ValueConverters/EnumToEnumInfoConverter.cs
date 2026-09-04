using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <inheritdoc cref="CommonValueConverters.EnumToEnumInfoConverter"/>
public class EnumToEnumInfoConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumToEnumInfoConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumToEnumInfoConverter();
    }
}
