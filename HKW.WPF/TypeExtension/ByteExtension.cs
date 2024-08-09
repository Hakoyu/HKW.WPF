using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Byte扩展
/// </summary>
[MarkupExtensionReturnType(typeof(byte))]
public class ByteExtension : MarkupExtension<byte>
{
    /// <inheritdoc/>
    public ByteExtension(byte value)
        : base(value)
    {
        Value = value;
    }
}
