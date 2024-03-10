using System.Windows;

namespace HKW.WPF.Converters;

/// <summary>
/// 可反转的多值转换器
/// </summary>
/// <typeparam name="TConverter">转换器类型</typeparam>
public abstract class InvertibleMultiValueConverterBase<TConverter>
    : MultiValueConverterBase<TConverter>
    where TConverter : InvertibleMultiValueConverterBase<TConverter>, new()
{
    /// <summary>
    ///
    /// </summary>
    public static readonly DependencyProperty IsInvertedProperty = DependencyProperty.Register(
        nameof(IsInverted),
        typeof(bool),
        typeof(InvertibleMultiValueConverterBase<TConverter>)
    );

    /// <summary>
    /// 是反转的
    /// </summary>
    public bool IsInverted
    {
        get => (bool)GetValue(IsInvertedProperty);
        set => SetValue(IsInvertedProperty, value);
    }
}
