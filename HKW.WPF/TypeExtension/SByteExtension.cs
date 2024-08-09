using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// SByte扩展
/// </summary>
[MarkupExtensionReturnType(typeof(sbyte))]
public class SByteExtension : MarkupExtension<sbyte>
{
    /// <inheritdoc/>
    public SByteExtension(sbyte value)
        : base(value)
    {
        Value = value;
    }
}
