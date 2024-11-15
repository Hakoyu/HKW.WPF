using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 全部为布尔值到能见度转换器
/// </summary>
public class AllBoolToVisibilityMultiConverter : AllBoolToValueMultiConverter<Visibility>
{
    /// <inheritdoc/>
    public AllBoolToVisibilityMultiConverter()
    {
        TrueValue = Visibility.Visible;
        FalseValue = Visibility.Collapsed;
        NullValue = Visibility.Collapsed;
        DefaultResult = Visibility.Collapsed;
    }
}
