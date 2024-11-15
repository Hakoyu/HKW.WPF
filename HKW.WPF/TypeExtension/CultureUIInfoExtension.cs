using System.Globalization;
using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// CultureUIInfo扩展
/// </summary>
[MarkupExtensionReturnType(typeof(CultureInfo))]
public class CultureUIInfoExtension : MarkupExtension<CultureInfo>
{
    /// <inheritdoc/>
    public CultureUIInfoExtension()
        : base(CultureInfo.CurrentUICulture)
    {
        Value = CultureInfo.CurrentUICulture;
    }

    /// <inheritdoc/>
    public CultureUIInfoExtension(CultureInfo value)
        : base(value)
    {
        Value = value;
    }
}
