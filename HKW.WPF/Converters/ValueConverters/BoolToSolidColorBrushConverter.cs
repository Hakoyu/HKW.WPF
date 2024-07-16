using System.Windows.Media;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到纯色笔刷转换器
/// </summary>
public class BoolToSolidColorBrushConverter
    : BoolToValueConverter<SolidColorBrush, BoolToSolidColorBrushConverter>
{
    /// <summary>
    ///
    /// </summary>
    public BoolToSolidColorBrushConverter()
    {
        TrueValue = Brushes.White;
        FalseValue = Brushes.Black;
    }
}
