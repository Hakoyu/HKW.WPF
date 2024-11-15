using System.Globalization;
using System.Windows;
using HKW.CommonValueConverters;
using HKW.HKWUtils;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到枚举信息转换器
/// </summary>
public class EnumToEnumInfoConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public EnumToEnumInfoConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumToEnumInfoConverter();
    }
}
