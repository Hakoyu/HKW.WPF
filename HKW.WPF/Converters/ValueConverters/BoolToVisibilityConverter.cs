using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到可见度转换器
/// </summary>
public class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
{
    /// <inheritdoc/>
    public BoolToVisibilityConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
    }
}
