using System.Globalization;
using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// CultureInfo扩展
/// </summary>
[MarkupExtensionReturnType(typeof(CultureInfo))]
public class CultureInfoExtension : MarkupExtension<CultureInfo>
{
    /// <inheritdoc/>
    public CultureInfoExtension()
        : base(CultureInfo.CurrentCulture)
    {
        Value = CultureInfo.CurrentCulture;
    }

    /// <inheritdoc/>
    public CultureInfoExtension(CultureInfo value)
        : base(value)
    {
        Value = value;
    }
}
