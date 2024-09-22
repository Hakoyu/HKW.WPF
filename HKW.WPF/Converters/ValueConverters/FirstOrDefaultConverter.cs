using System;
using System.Collections;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 第一个或默认转换器
/// </summary>
public class FirstOrDefaultConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public FirstOrDefaultConverter()
    {
        CommonValueConverter = new CommonValueConverters.FirstOrDefaultResultConverter();
    }
}
