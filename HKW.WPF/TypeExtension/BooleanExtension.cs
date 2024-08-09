using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Boolean扩展
/// </summary>
[MarkupExtensionReturnType(typeof(bool))]
public class BooleanExtension : MarkupExtension<bool>
{
    /// <inheritdoc/>
    public BooleanExtension(bool value)
        : base(value)
    {
        Value = value;
    }
}
