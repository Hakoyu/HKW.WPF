using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Single扩展
/// </summary>
[MarkupExtensionReturnType(typeof(float))]
public class SingleExtension : MarkupExtension<float>
{
    /// <inheritdoc/>
    public SingleExtension(float value)
        : base(value)
    {
        Value = value;
    }
}
