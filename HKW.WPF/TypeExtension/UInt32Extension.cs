using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// UInt32扩展
/// </summary>
[MarkupExtensionReturnType(typeof(uint))]
public class UInt32Extension : MarkupExtension<uint>
{
    /// <inheritdoc/>
    public UInt32Extension(uint value)
        : base(value)
    {
        Value = value;
    }
}
