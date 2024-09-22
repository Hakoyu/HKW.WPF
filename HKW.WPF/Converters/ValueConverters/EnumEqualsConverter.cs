using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 枚举到布尔转换器
/// </summary>
public class EnumEqualsConverter : InvertibleValueConverterBase
{
    /// <inheritdoc/>
    public EnumEqualsConverter()
    {
        CommonValueConverter = new CommonValueConverters.EnumEqualsConverter();
    }
}
