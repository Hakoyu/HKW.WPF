using System;
using System.Globalization;

namespace HKW.WPF.Converters;

/// <summary>
/// 版本到字符串转换器
/// <example>
/// [1] Major Version
/// [2] Minor Version
/// [3] Build Number
/// [4] Revision
/// </example>
/// </summary>
public class VersionToStringConverter : ValueConverterBase
{
    /// <inheritdoc/>
    public VersionToStringConverter()
    {
        CommonValueConverter = new CommonValueConverters.VersionToStringConverter();
    }
}
