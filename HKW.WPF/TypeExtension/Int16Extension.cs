using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Int16扩展
/// </summary>
[MarkupExtensionReturnType(typeof(short))]
public class Int16Extension : MarkupExtension<short>
{
    /// <inheritdoc/>
    public Int16Extension(short value)
        : base(value)
    {
        Value = value;
    }
}
