using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到字体粗细转换器
/// </summary>
public class BoolToFontWeightConverter : BoolToValueConverter<FontWeight, BoolToFontWeightConverter>
{
    /// <inheritdoc/>
    public BoolToFontWeightConverter()
    {
        TrueValue = FontWeights.Bold;
        FalseValue = FontWeights.Normal;
    }
}
