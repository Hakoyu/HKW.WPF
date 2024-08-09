using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// UInt64扩展
/// </summary>
[MarkupExtensionReturnType(typeof(ulong))]
public class UInt64Extension : MarkupExtension<ulong>
{
    /// <inheritdoc/>
    public UInt64Extension(ulong value)
        : base(value)
    {
        Value = value;
    }
}
