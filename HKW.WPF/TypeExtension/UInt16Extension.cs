using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// UInt16扩展
/// </summary>
[MarkupExtensionReturnType(typeof(ushort))]
public class UInt16Extension : MarkupExtension<ushort>
{
    /// <inheritdoc/>
    public UInt16Extension(ushort value)
        : base(value)
    {
        Value = value;
    }
}
