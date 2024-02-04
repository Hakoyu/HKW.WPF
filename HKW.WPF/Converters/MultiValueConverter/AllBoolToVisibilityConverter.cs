using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部为布尔值到能见度转换器
/// </summary>
public class AllBoolToVisibilityConverter
    : AllBoolToValueConverter<Visibility, AllBoolToVisibilityConverter>
{
    /// <inheritdoc/>
    public AllBoolToVisibilityConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
    }
}
