using System;
using System.Diagnostics;

namespace HKW.WPF.Converters;

/// <summary>
/// 调试转换器
/// </summary>
public class DebugConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public DebugConverter()
    {
        CommonValueConverter = new CommonValueConverters.DebugConverter();
    }
}
