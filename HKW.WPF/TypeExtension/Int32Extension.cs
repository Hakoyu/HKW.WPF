using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace HKW.WPF.TypeExtension;

/// <summary>
/// Int32扩展
/// </summary>
[MarkupExtensionReturnType(typeof(int))]
public class Int32Extension : MarkupExtension<int>
{
    /// <inheritdoc/>
    public Int32Extension(int value)
        : base(value)
    {
        Value = value;
    }
}
