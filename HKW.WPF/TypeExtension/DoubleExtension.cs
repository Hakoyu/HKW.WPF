using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Double扩展
/// </summary>
[MarkupExtensionReturnType(typeof(double))]
public class DoubleExtension : MarkupExtension<double>
{
    /// <inheritdoc/>
    public DoubleExtension(double value)
        : base(value)
    {
        Value = value;
    }
}
