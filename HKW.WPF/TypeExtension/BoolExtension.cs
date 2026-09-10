using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Bool扩展
/// </summary>
[MarkupExtensionReturnType(typeof(bool))]
public class BoolExtension : MarkupExtension<bool>
{
    /// <inheritdoc/>
    public BoolExtension(bool value)
        : base(value) { }
}
