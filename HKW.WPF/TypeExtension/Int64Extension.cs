using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Int64扩展
/// </summary>
[MarkupExtensionReturnType(typeof(long))]
public class Int64Extension : MarkupExtension<long>
{
    /// <inheritdoc/>
    public Int64Extension(long value)
        : base(value)
    {
        Value = value;
    }
}
