using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 布尔到参数值转换器
/// </summary>
public abstract class BoolToParameterConverterBase<TConverter> : ValueConverterBase<TConverter>
    where TConverter : ValueConverterBase<TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty SeparatorProperty = DependencyProperty.Register(
        nameof(Separator),
        typeof(char),
        typeof(TConverter),
        new(',')
    );

    /// <summary>
    /// 分割符
    /// </summary>
    public char Separator
    {
        get => (char)GetValue(SeparatorProperty);
        set => SetValue(SeparatorProperty, value);
    }
}
