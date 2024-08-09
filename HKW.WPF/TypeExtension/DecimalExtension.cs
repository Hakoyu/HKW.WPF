using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Decimal扩展
/// </summary>
[MarkupExtensionReturnType(typeof(decimal))]
public class DecimalExtension : MarkupExtension<decimal>
{
    /// <inheritdoc/>
    public DecimalExtension(decimal value)
        : base(value)
    {
        Value = value;
    }
}
